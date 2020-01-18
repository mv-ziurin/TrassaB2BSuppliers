using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public class GroupOwner
    {
        public int Id { get; set; }

        public int NaturalPersonId { get; set; }

        public virtual ICollection<CounterpartyGroupOwner> CounterpartyGroupOwners { get; set; }

        public GroupOwner()
        {
            this.CounterpartyGroupOwners = new HashSet<CounterpartyGroupOwner>();
        }
    }
}
