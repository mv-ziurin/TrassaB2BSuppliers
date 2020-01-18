using System;

namespace NaturalPersons.Database.Models
{
    public class IdentityDocument
    {
        public enum DocumentType
        {
            Passport,
            InternationalPassport,
            DriversLicense
        }

        public enum SexType
        {
            Male,
            Female
        }

        public int Id { get; set; }

        public int NaturalPersonId { get; set; }

        public DocumentType Type { get; set; }

        public int? Number { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public SexType Sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfIssue { get; set; }

        public string Authority { get; set; }

        public DateTime DateOfExpiration { get; set; }

        public int? RegistrationAddressId { get; set; }

        public virtual NaturalPerson NaturalPerson { get; set; }
    }
}
