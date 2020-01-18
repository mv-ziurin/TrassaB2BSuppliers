using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Индивидуальные предприниматели
    /// </summary>
    public class IndividualEntrepreneur
    {
        public int Id { get; set; }

        public int NaturalPersonId { get; set; }

        public long Ogrnip { get; set; }

        public long Inn { get; set; }

        public DateTime EntryDateInOgrnip { get; set; }

        public DateTime TerminationDate { get; set; }

        public virtual IeAccountingInfo IeAccountingInfo { get; set; }

        public virtual IeAuthorityInfo IeAuthorityInfo { get; set; }

        public virtual IeFullName IeFullName { get; set; }

        public virtual IeNationality IeNationality { get; set; }

        public virtual IeRegistrationInfo IeRegistrationInfo { get; set; }

        public virtual IeTerminationInfo IeTerminationInfo { get; set; }
    }
}
