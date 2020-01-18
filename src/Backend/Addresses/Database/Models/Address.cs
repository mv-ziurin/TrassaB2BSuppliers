using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addresses.Database.Models
{
    public class Address
    {
        public int Id { get; set; }

        public int StreetId { get; set; }

        public int DistrictId { get; set; }

        public string House { get; set; }

        public string HouseBlock { get; set; }

        public string Building { get; set; }

        public string HomeOwnership { get; set; }

        public string Ownership { get; set; }

        public string Apartment { get; set; }

        public int Postcode { get; set; }

        public int Entrance { get; set; }

        public string Intercom { get; set; }

        public int Floor { get; set; }

        public string Pavilion { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Comment { get; set; }

        public virtual Street Street { get; set; }

        public virtual District District { get; set; }
    }
}
