using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public class CounterpartyGroupOwner
    {
        public int Id { get; set; }

        public int CounterpartyId { get; set; }
        
        public int GroupOwnerId { get; set; }

        public virtual Counterparty Counterparty { get; set; }

        public virtual GroupOwner GroupOwner { get; set; }
    }
}
