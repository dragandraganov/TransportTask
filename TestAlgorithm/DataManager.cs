using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace TestAlgorithm
{
    public class DataManager
    {
        public void InitializeGraph(ICollection<Connection> connections, ICollection<Connection> startConnections)
        {
            foreach (var conn in connections)
            {
                if (!startConnections.Contains(conn))
                {
                    conn.DepartureStop.MinutesToArrive = int.MaxValue;
                }

                else
                {
                    conn.DepartureStop.MinutesToArrive = 0;
                }

                conn.ArrivalStop.MinutesToArrive = int.MaxValue;
            }
        }
    }
}
