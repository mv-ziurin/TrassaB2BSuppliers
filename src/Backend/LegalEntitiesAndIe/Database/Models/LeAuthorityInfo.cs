using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// по месту нахождения юр.лиц
    /// </summary>
    public class LeAuthorityInfo
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string Name { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrul { get; set; }

        public int LegalEntityId { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
    }
}
