using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Tools
{
    public class ExcelTableReader
    {
        public static ICollection<Tuple<string, ICollection<DataRow>>> ReadData(string pathToExcelFile)
        {
            var result = new List<Tuple<string, ICollection<DataRow>>>();
            string strExcelConn = String.Format("{0}{1}{2}", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", pathToExcelFile, ";Extended Properties=\"Excel 12.0 xml;HDR=Yes\"");

            using (var conObj = new OleDbConnection(strExcelConn))
            {
                conObj.Open();
                var data = conObj.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (data == null)
                {
                    return null;
                }

                var excelSheets = new List<string>();

                // Add the sheet name to the string array.
                foreach (DataRow row in data.Rows)
                {
                    var sheetName = row["TABLE_NAME"].ToString();

                    string cmdText = String.Format("SELECT * FROM [{0}]", sheetName);
                    using (OleDbCommand cmd = new OleDbCommand(cmdText, conObj))
                    {
                        OleDbDataAdapter oleda = new OleDbDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        oleda.Fill(ds, "Table");
                        DataTable dt = new DataTable();
                        dt = ds.Tables["Table"];
                        var newTable = dt.AsEnumerable()
                            .ToList();

                        var sheetResult = new Tuple<string, ICollection<DataRow>>(sheetName, newTable);
                        result.Add(sheetResult);
                    };
                }

                return result;
            };
        }
    }
}
