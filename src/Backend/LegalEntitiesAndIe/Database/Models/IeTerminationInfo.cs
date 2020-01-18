using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения о прекращении детельности
    /// в качестве ИП
    /// </summary>
    public class IeTerminationInfo
    {
        public int Id { get; set; }

        public string TerminationMethod { get; set; }

        public DateTime TerminationDate { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrip { get; set; }

        public string RegistrationAuthorityName { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
