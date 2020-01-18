using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// по месту жительства ИП
    /// </summary>
    public class IeAuthorityInfo
    {
        public int Id { get; set; }

        public string RegistrationAuthorityName { get; set; }

        public int RegistrationAuthorityId { get; set; }

        // Какое отчество????
        public string Patronymic { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateIntEgrip { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
