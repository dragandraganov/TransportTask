using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public class GlobalConstants
    {
        public const int NumberOfVehicles = 10;
        public const int NumberOfStops = 50;
        public const int MinStopsPerTrip = 10;
        public const int MaxStopsPerTrip = 20;
        public const double AverageMinutesForConnection = 20;
        public const double MaxDeviationInMinutes = 2;
        public const int MaximumJourneyTimeinMinutes = 120;

        public const string PathToExcelFile = @"..\..\..\Resources\Timetables.xlsx";
    }
}
