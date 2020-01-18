using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения о регистрации ИП
    /// </summary>
    public class IeRegistrationInfo
    {
        public int Id { get; set; }

        public long Ogrnip { get; set; }

        public DateTime EntryDateInOgrnip { get; set; }

        public long OldRegistrationNumber { get; set; }

        public DateTime OldRegistrationDate { get; set; }

        public string OldRegistrationAuthority { get; set; }

        public string ReistrationAuthorityName { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
