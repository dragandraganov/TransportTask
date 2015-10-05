﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace TestAlgorithm
{
    public class DijkstraAlgorithm
    {
        public List<Connection> FindShortestPath(string startStopName, string targetStopName, List<Connection> actualConnections, List<Connection> remainingConnections, out Stop endStop, out Stack<Stop> shortestPath)
        {
            while (actualConnections.Count() > 0)
            {
                remainingConnections = remainingConnections
                    .OrderBy(c => c.DepartureStop.MinutesToArrive)
                    .ToList();

                if (remainingConnections.First().DepartureStop.MinutesToArrive == int.MaxValue)
                {
                    break;
                }

                var nextStop = remainingConnections
                    .First()
                    .DepartureStop;

                var busLine = remainingConnections
                    .First()
                    .BusLine;

                var arrivalTime = remainingConnections
                   .First().ArrivalTime;

                var nextConnections = remainingConnections
                    .Where(c => c.DepartureStop == nextStop)
                    .ToList();

                foreach (var conn in nextConnections)
                {
                    var additionalTime = 0.00;

                    if (conn.BusLine != busLine)
                    {
                        additionalTime = (conn.DepartureTime.TimeOfDay - arrivalTime.TimeOfDay).TotalMinutes;
                    }

                    if (conn.ArrivalStop.MinutesToArrive > (conn.DepartureStop.MinutesToArrive + conn.TimeOfArrival + additionalTime))
                    {
                        conn.ArrivalStop.MinutesToArrive = conn.DepartureStop.MinutesToArrive + conn.TimeOfArrival + additionalTime;
                        conn.ArrivalStop.PreviousStop = conn.DepartureStop;
                        conn.ArrivalStop.BusLineInShortestPath = conn.BusLine;
                    }

                    remainingConnections.Remove(conn);
                }

            }

            endStop = actualConnections
                .FirstOrDefault(c => c.DepartureStop.Name == targetStopName)
                .DepartureStop;

            shortestPath = new Stack<Stop>();
            shortestPath.Push(endStop);
            var previousStop = endStop.PreviousStop;

            while (previousStop.Name != startStopName)
            {
                shortestPath.Push(previousStop);
                previousStop = previousStop.PreviousStop;
            }
            return remainingConnections;
        }
    }
}
