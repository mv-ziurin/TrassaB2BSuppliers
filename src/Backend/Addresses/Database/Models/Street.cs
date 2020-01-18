using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StreetTypeId { get; set; }

        public int LocalityId { get; set; }

        public virtual Locality Locality { get; set; }

        public virtual StreetType StreetType { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public Street()
        {
            this.Addresses = new HashSet<Address>();
        }

    }
}
