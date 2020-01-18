using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения об учёте юр.лиц в налоговом органе
    /// </summary>
    public class LeAccountingInfo
    {
        public int Id { get; set; }

        public long Inn { get; set; }

        public long Kpp { get; set; }

        public DateTime RegistationDate { get; set; }

        public string TaxAuthorityName { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrul { get; set; }

        public int LegalEntityId { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
    }
}
