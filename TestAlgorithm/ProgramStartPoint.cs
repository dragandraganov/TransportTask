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

            //var startTime = DateTime.Now;
            var startTime = new DateTime(2015, 09, 12, 10, 50, 00);
            var startDayTime = startTime.TimeOfDay;
            var startStopName = "МУЗЕЯ";
            var targetStopName = "Нова7";

            var dataGenerator = new DataGenerator();
            var allConnections = dataGenerator.CreateConnections();

            var actualConnections = allConnections
                .Where(c => c.DepartureTime.TimeOfDay > startDayTime)
                .Where(c => c.ArrivalTime.TimeOfDay < startTime.AddMinutes(GlobalConstants.MaximumJourneyTimeinMinutes).TimeOfDay)
                .ToList();

            var startStop = actualConnections
                .FirstOrDefault(c => c.DepartureStop.Name == startStopName)
                .DepartureStop;

            startStop.ArrivalTime = startTime;

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

            //Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(String.Format("{0} минути", endStop.MinutesToArrive));
            Console.Write(String.Format("От спирка {0} ", startStopName));
            Console.WriteLine(String.Join("->", shortestPath.Select(s => String.Format("Автобус {0} до спирка {1}", s.BusLineInShortestPath.Number, s.Name))));
        }
    }
}
