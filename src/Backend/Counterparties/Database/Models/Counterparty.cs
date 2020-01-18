using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public enum OrganizationType
    {
        IndividualEntrepreneur,
        LegalEntity
    }

    public enum RoleType
    {
        Partner,
        Supplier
    }

    public class Counterparty
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public OrganizationType OrganizationForm { get; set; }

        public RoleType Role { get; set; }

        public string FullName { get; set; }

        public string ShortName { get; set; }

        public int EmployeesNumber { get; set; }

        public int Age { get; set; }

        public int GroupNameId { get; set; }

        public virtual GroupName GroupName { get; set; }

        public virtual ICollection<CounterpartyGroupOwner> CounterpartyGroupOwners { get; set; }

        public virtual ICollection<Filial> Filials { get; set; }

        public Counterparty()
        {
            this.Filials = new HashSet<Filial>();
            this.CounterpartyGroupOwners = new HashSet<CounterpartyGroupOwner>();
        }
    }
}
