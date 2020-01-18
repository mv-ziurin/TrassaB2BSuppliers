using System.Collections.Generic;

namespace LegalWorkers.Database.Models
{
    public class UQPDSection
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public UQPDSection()
        {
            Posts = new HashSet<Post>();
        }
    }
}
