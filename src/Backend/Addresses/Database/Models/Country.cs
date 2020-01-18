using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alpha2Code { get; set; }

        public string Alpha3Code { get; set; }

        public virtual ICollection<Region> Regions { get; set; }

        public virtual ICollection<Locality> Localities { get; set; }

        public Country()
        {
            this.Regions = new HashSet<Region>();
            this.Localities = new HashSet<Locality>();
        }
    }
}
