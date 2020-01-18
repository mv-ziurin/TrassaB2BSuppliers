using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparties.Database.Models
{
    public class FilialPhoto
    {
        public int Id { get; set; }

        public int FilialId { get; set; }

        public int FileId { get; set; }

        public virtual Filial Filial { get; set; }
    }
}
