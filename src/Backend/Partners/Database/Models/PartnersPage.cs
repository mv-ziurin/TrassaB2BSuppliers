using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public class PartnersPage
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public int PartnerId { get; set; }

        public int SocialNetWorkId { get; set; }

        public virtual Partner Partner { get; set; }

        public virtual SocialNetwork SocialNetwork { get; set; }
    }
}
