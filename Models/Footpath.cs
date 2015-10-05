using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Footpath
    {
        public Footpath()
        {
            this.Journeys=new HashSet<Journey>();
        }

        public int Id { get; set; }

        public virtual Stop FirstStop { get; set; }

        public virtual Stop SecondStop { get; set; }

        public int MinutesToWalk { get; set; }

        public virtual ICollection<Journey> Journeys { get; set; }

    }
}
