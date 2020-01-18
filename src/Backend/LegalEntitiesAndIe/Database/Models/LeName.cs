using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Наименование юр.лиц
    /// </summary>
    public class LeName
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrul { get; set; }

        public int LegalEntityId { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
    }
}
