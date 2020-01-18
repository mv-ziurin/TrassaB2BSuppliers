using System;
using System.Collections.Generic;

namespace NaturalPersons.Database.Models
{
    public class NaturalPerson
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<IdentityDocument> IdentityDocuments { get; set; }

        public NaturalPerson()
        {
            this.IdentityDocuments = new HashSet<IdentityDocument>();
        }

    }
}
