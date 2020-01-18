using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class Locality
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LocalityTypeId { get; set; }

        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public virtual Region Region { get; set; }

        public virtual Country Country { get; set; }

        public virtual LocalityType LocalityType { get; set; }

        public virtual ICollection<Street> Streets { get; set; }

        public virtual ICollection<District> Districts { get; set; }

        public Locality()
        {
            this.Streets = new HashSet<Street>();
            this.Districts = new HashSet<District>();
        }
    }
}
