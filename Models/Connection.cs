using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Connection
    {
        public Connection()
        {
            this.Trips = new HashSet<Trip>();
            this.Journeys = new HashSet<Journey>();
        }

        public int Id { get; set; }

        public virtual Stop DepartureStop { get; set; }

        public virtual Stop ArrivalStop { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public double TimeOfArrival
        {
            get
            {
                return (this.ArrivalTime - this.DepartureTime).TotalMinutes;
            }
        }

        public virtual BusLine BusLine { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Journey> Journeys { get; set; }
    }
}
