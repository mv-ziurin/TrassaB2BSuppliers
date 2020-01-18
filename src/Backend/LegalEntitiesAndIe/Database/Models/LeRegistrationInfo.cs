using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения о регистрации юр.лиц
    /// </summary>
    public class LeRegistrationInfo
    {
        public int Id { get; set; }

        public long Ogrn { get; set; }

        public DateTime AssignmentDateOfOgrn { get; set; }

        public long OldRegistrationNumber { get; set; }

        public string OldRegistrationAuthority { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrul { get; set; }

        public int LegalEntityId { get; set; }

        public virtual LegalEntity LegalEntity { get; set; }
    }
}
