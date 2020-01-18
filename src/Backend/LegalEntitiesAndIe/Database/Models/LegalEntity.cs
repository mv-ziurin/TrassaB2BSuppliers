using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Юридические лица
    /// </summary>
    public class LegalEntity
    {
        public int Id { get; set; }

        public DateTime AssignmentDateOfOgrn { get; set; }

        public DateTime TerminationDate { get; set; }

        public string Head { get; set; }

        public string Founder { get; set; }

        public int LegalAddressId { get; set; }

        public int PostAddressId { get; set; }

        public int CentralOfficeAddressId { get; set; }

        public virtual LeName LeName { get; set; }

        public virtual LeLocationAddress LeLocationAddress { get; set; }

        public virtual LeRegistrationInfo LeRegistrationInfo { get; set; }

        public virtual LeAccountingInfo LeAccountingInfo { get; set; }

        public virtual LeAuthorityInfo LeAuthorityInfo { get; set; }
    }
}
