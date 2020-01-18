using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int FileSedId { get; set; }

        public double Size { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual SalesPointPhoto SalesPointPhoto { get; set; }
    }
}
