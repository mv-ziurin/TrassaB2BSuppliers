using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public class GroupName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Counterparty> Counterparties { get; set; }

        public GroupName()
        {
            this.Counterparties = new HashSet<Counterparty>();
        }
    }
}
