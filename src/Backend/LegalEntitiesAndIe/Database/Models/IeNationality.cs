using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Гражданство ИП
    /// </summary>
    public class IeNationality
    {
        public int Id { get; set; }

        public string Nationality { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrip { get; set; }

        public string RegistrationAuthorityName { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
