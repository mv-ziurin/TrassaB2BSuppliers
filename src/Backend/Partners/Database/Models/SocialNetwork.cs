using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public class SocialNetwork
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public virtual ICollection<PartnersPage> PartnersPages { get; set; }

        public SocialNetwork()
        {
            this.PartnersPages = new HashSet<PartnersPage>();
        }
    }
}
