using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public class Filial
    {
        public int Id { get; set; }

        public int CountepartyId { get; set; }

        public int AddressId { get; set; }

        public string Name { get; set; }

        public virtual Counterparty Counterparty { set; get; }

        public virtual ICollection<FilialCollaborator> FilialCollaborators { get; set; }

        public virtual ICollection<FilialPhoto> FilialPhotos { get; set; }

        public Filial()
        {
            this.FilialCollaborators = new HashSet<FilialCollaborator>();
            this.FilialPhotos = new HashSet<FilialPhoto>();
        }

    }
}
