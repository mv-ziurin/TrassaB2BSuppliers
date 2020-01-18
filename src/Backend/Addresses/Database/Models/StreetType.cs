using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class StreetType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Street Street { get; set; }
    }
}
