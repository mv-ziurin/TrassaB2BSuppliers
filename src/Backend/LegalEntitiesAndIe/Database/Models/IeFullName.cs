using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    public enum SexType { Male, Female }

    /// <summary>
    /// ФИО ОП
    /// </summary>
    public class IeFullName
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public SexType Sex { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateIntEgrip { get; set; }

        public string RegistrationAuthorityName { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
