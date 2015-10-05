using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Journey
    {
        public Journey()
        {
            this.Connections = new HashSet<Connection>();
            this.Footpaths = new HashSet<Footpath>();
        }
        public int Id { get; set; }

        public virtual ICollection<Connection> Connections { get; set; }

        //Because our footpaths are transitively closed, a journey never contains two subsequent footpaths.
        public virtual ICollection<Footpath> Footpaths { get; set; }
    }
}
