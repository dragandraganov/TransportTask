using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;

namespace TestAlgorithm
{
    class ProgramStartPoint
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now.TimeOfDay;
            var startStopName = "МЛЕКОЗАВОД-2";
            var targetStopName = "МУЗЕЯ";

            var dataGenerator = new DataGenerator();
            var allConnections = dataGenerator.CreateConnections();

            var actualConnections = allConnections
                .Where(c => c.DepartureTime.TimeOfDay > startTime)
                .Where(c => c.ArrivalTime.TimeOfDay < DateTime.Now.AddMinutes(GlobalConstants.MaximumJourneyTimeinMinutes).TimeOfDay)
                .ToList();

            var startConnections = actualConnections
                .Where(c => c.DepartureStop.Name == startStopName)
                .OrderBy(c => c.DepartureTime)
                .ToList();

            var dataManager = new DataManager();

            dataManager.InitializeGraph(actualConnections, startConnections);

            var remainingConnections = actualConnections.OrderBy(c => c.DepartureStop.MinutesToArrive).ToList();

            Stop endStop;
            Stack<Stop> shortestPath;

            var algortihm = new DijkstraAlgorithm();
            remainingConnections = algortihm.FindShortestPath(startStopName, targetStopName, actualConnections, remainingConnections, out endStop, out shortestPath);

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(String.Format("{0} минути", endStop.MinutesToArrive));
            Console.WriteLine(String.Join("->", shortestPath.Select(s => String.Format("Автобус {0} до спирка {1}", s.BusLineInShortestPath.Number, s.Name))));
        }
    }
}
