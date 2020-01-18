using System.Collections.Generic;

namespace LegalWorkers.Database.Models
{
    public class Worker
    {
        public int Id { get; set; }

        public int NaturalPersonId { get; set; }

        public int LegalEntityId { get; set; }

        public int PostId { get; set; }

        public int DepartmentId { get; set; }

        public int DirectorId { get; set; }

        public virtual Worker Director { get; set; }

        public virtual Post Post { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }

        public virtual ICollection<WorkerFunction> Functions { get; set; }

        public Worker()
        {
            Workers = new HashSet<Worker>();
            Functions = new HashSet<WorkerFunction>();
        }
    }
}
