using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public class PartnersContactDetail
    {
        public int Id { get; set; }

        public int ContactDetailId { get; set; }

        public int PartnerId { get; set; }

        public int SalesPointId { get; set; }

        public virtual SalesPoint SalesPoint { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
