using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Адреса местонахождения юр.лиц
    /// </summary>
    public class LeLocationAddress
    {
        public int Id { get; set; }

        public int AddressesId { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrul { get; set; }

        public int LegalEntityId { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
    }
}
