using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public enum Type
    {
        Collective,
        Individual
    }

    public class PurchasingDecisionModel
    {
        public int Id { get; set; }

        public int NaturalPersonId { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }

        public PurchasingDecisionModel()
        {
            this.Partners = new HashSet<Partner>();
        }
    }
}
