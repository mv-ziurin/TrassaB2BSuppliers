using System;

namespace LegalWorkers.Database.Models
{
    public class WorkerFunction
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int FunctionId { get; set; }

        public string Comment { get; set; }

        public int AuthorId { get; set; } //FK to NaturalPerson пока int

        public DateTime DateOfRecordCreation { get; set; }

        public DateTime DateOfRecordRemoval { get; set; }

        public virtual EmployeeFunction Function { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
