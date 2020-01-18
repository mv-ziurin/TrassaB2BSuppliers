using System.Collections.Generic;

namespace LegalWorkers.Database.Models
{
    public class EmployeeFunction
    {
        public int Id { get; set; }

        public string CounteragentType { get; set; } //Пока так. Наверно будет enum

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<WorkerFunction> Functions { get; set; }

        public EmployeeFunction()
        {
            Functions = new HashSet<WorkerFunction>();
        }
    }
}
