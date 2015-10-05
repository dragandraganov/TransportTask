using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Trip
    {
        public Trip()
        {
            this.Connections = new HashSet<Connection>();
        }

        public int Id { get; set; }

        public virtual BusLine Vehicle { get; set; }

        public virtual ICollection<Connection> Connections { get; set; }
    }
}
