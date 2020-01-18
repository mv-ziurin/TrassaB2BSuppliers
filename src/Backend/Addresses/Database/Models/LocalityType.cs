using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class LocalityType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Locality Locality { get; set; }
    }
}
