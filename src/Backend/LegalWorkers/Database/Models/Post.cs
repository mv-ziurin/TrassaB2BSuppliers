using System.Collections.Generic;

namespace LegalWorkers.Database.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SectionId { get; set; }

        public virtual UQPDSection Section { get; set; } // eдиный квалификационный справочник должностей

        public virtual ICollection<Worker> Workers { get; set; }

        public Post()
        {
            Workers = new HashSet<Worker>();
        }
    }
}
