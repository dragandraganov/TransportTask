using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Stop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double MinutesToArrive { get; set; }

        public virtual Stop PreviousStop { get; set; }

        public virtual BusLine BusLineInShortestPath { get; set; }
    }
}
