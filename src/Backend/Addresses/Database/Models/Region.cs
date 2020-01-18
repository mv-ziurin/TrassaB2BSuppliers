using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Locality> Localities { get; set; }

        public Region()
        {
            this.Localities = new HashSet<Locality>();
        }
    }
}
