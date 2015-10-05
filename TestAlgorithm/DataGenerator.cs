using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using Models;

namespace TestAlgorithm
{
    public class DataGenerator
    {
        public List<Connection> CreateConnections()
        {
            var allTimeTableData = ExcelTableReader.ReadData(GlobalConstants.PathToExcelFile);
            var allVehicles = new List<BusLine>();
            var allStops = new List<Stop>();
            var allConnections = new List<Connection>();
            var allTrips = new List<Trip>();

            foreach (var table in allTimeTableData)
            {
                var busLine = new BusLine();
                var vehicleNumber = table.Item1.Replace("'", string.Empty);
                vehicleNumber = vehicleNumber.Remove(vehicleNumber.Length - 1);
                if (allVehicles.FirstOrDefault(v => v.Number == vehicleNumber) == null)
                {
                    busLine = new BusLine() { Number = vehicleNumber };
                    allVehicles.Add(busLine);
                }

                var rows = table.Item2;

                foreach (var stopName in rows.Select(r => r[0]))
                {
                    if (allStops.FirstOrDefault(s => s.Name == stopName.ToString()) == null)
                    {
                        var stop = new Stop() { Name = stopName.ToString() };
                        allStops.Add(stop);
                    }
                }

                for (int i = 1; i < rows.Count(); i++)
                {
                    var previousRow = rows.ToArray()[i - 1];
                    var currentRow = rows.ToArray()[i];

                    var departureStopName = previousRow[0].ToString();
                    var departureStop = allStops.FirstOrDefault(s => s.Name == departureStopName);
                    var arrivalStopName = currentRow[0].ToString();
                    var arrivalStop = allStops.FirstOrDefault(s => s.Name == arrivalStopName);
                    for (int j = 2; j < currentRow.ItemArray.Count(); j++)
                    {
                        var arrivalTime = Convert.ToDateTime(currentRow[j]);
                        var departureTime = Convert.ToDateTime(previousRow[j]);

                        //var arrivalTime = new DateTime();
                        //if (!String.IsNullOrEmpty(currentRow[j].ToString()))
                        //{
                        //    arrivalTime = Convert.ToDateTime(currentRow[j]);
                        //}
                        //else
                        //{
                        //    continue;
                        //}

                        //var nonEmptyRow = i;

                        //while (String.IsNullOrEmpty(rows.ToArray()[nonEmptyRow][j].ToString()))
                        //{
                        //    nonEmptyRow--;
                        //}
                        //var departureTime = Convert.ToDateTime(rows.ToArray()[nonEmptyRow][j]);

                        var connection = new Connection();
                        connection.DepartureStop = departureStop;
                        connection.ArrivalStop = arrivalStop;
                        connection.DepartureTime = departureTime;
                        connection.ArrivalTime = arrivalTime;
                        connection.BusLine = busLine;

                        allConnections.Add(connection);
                    }


                }
            }
            return allConnections;
        }

        public void SetRandomData()
        {
            //random generating data
            //var generator = new RandomGenerator();
            //var vehiclesNumbers = new HashSet<string>();
            //var stopsNames = new HashSet<string>();
            //var allVehicles = new List<Vehicle>();
            //var allStops = new List<Stop>();
            //var allTrips = new List<Trip>();

            //while (vehiclesNumbers.Count() < GlobalConstants.NumberOfVehicles)
            //{
            //    vehiclesNumbers.Add(generator.ReturnRandomNumber(1, GlobalConstants.NumberOfVehicles + 1).ToString());
            //}

            //vehiclesNumbers.ToList().ForEach(vn => allVehicles.Add(new Vehicle { Number = vn }));

            //while (stopsNames.Count() < GlobalConstants.NumberOfStops)
            //{
            //    stopsNames.Add(generator.ReturnRandomString());
            //}

            //stopsNames.ToList().ForEach(s => allStops.Add(new Stop { Name = s }));


            //foreach (var vehicle in allVehicles)
            //{
            //    //no equal between minutes and decimal part, but it's not a big problem
            //    var startTripHour = Math.Round(generator.ReturnRandomDouble(5, 7), 2);
            //    var indexedStops = new HashSet<Tuple<Stop, int>>();
            //    var numberOfStops = generator.ReturnRandomNumber(GlobalConstants.MinStopsPerTrip, GlobalConstants.MaxStopsPerTrip);
            //    while (indexedStops.Count < numberOfStops)
            //    {
            //        var randomStopIndex = generator.ReturnRandomNumber(0, GlobalConstants.NumberOfStops);
            //        indexedStops.Add(new Tuple<Stop, int>(allStops[randomStopIndex], randomStopIndex));
            //    }

            //    var stopsInTrip = indexedStops.OrderBy(s => s.Item2).Select(s => allStops[s.Item2]).ToList();

            //    var connectionsInTrip = new List<Connection>();

            //    for (int i = 1; i < stopsInTrip.Count; i++)
            //    {
            //        var normalDeviation = generator.ReturnRandomDouble(0, GlobalConstants.MaxDeviationInMinutes / 60);
            //        var arrivalTime = Math.Round((startTripHour + GlobalConstants.AverageMinutesForConnection / 60 + normalDeviation), 2);
            //        connectionsInTrip.Add(new Connection() { DepartureStop = stopsInTrip[i - 1], ArrivalStop = stopsInTrip[i], DepartureTime = startTripHour, ArrivalTime = arrivalTime });
            //        startTripHour = arrivalTime;
            //    }

            //    var trip = new Trip() { Vehicle = vehicle, Connections = connectionsInTrip };
            //    allTrips.Add(trip);
            //}

            //Console.WriteLine(String.Join(", ", allVehicles.Select(v => v.Number)));
            //Console.WriteLine(String.Join(", ", allStops.Select(s => s.Name)));
            //var stopNames = allTrips[0].Connections.Select(c => c.ArrivalStop.Name);
            //Console.WriteLine(String.Format("{0} -> {1}", allTrips[0].Vehicle.Number, String.Join(", ", stopNames)));

            //var departureTimes = allTrips[0].Connections.Select(c => c.DepartureTime);
            //Console.WriteLine(String.Format("{0} -> {1} -> {2} connections", allTrips[0].Vehicle.Number, String.Join(", ", departureTimes), departureTimes.Count()));

            //var departureTimes1 = allTrips[1].Connections.Select(c => c.DepartureTime);
            //Console.WriteLine(String.Format("{0} -> {1} -> {2} connections", allTrips[1].Vehicle.Number, String.Join(", ", departureTimes1), departureTimes1.Count()));

        }
    }
}
