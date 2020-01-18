using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LegalEntitiesAndIe.Database.Models
{
    /// <summary>
    /// Сведения об учёте ИП в налоговом органе
    /// </summary>
    public class IeAccountingInfo
    {
        public int Id { get; set; }

        public long Inn { get; set; }

        public DateTime EntryDate { get; set; }

        public string TaxAuthorityName { get; set; }

        public long Grn { get; set; }

        public DateTime EntryDateInEgrip { get; set; }

        public string RegistrationAuthorityName { get; set; }

        public int IndividualEntrepreneurId { get; set; }

        public virtual IndividualEntrepreneur IndividualEntrepreneur { get; set; }
    }
}
