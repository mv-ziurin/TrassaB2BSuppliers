using Microsoft.EntityFrameworkCore;
using NaturalPersons.Database.Models;

namespace NaturalPersons.Database
{
    public class NaturalPersonsContext : DbContext
    {

        public NaturalPersonsContext(DbContextOptions<NaturalPersonsContext> options) : base(options) { }

        public DbSet<NaturalPerson> NaturalPersons { get; set; }

        public DbSet<IdentityDocument> IdentityDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("naturalPersons");

            // public keys
            modelBuilder.Entity<NaturalPerson>().HasKey(a => a.Id);
            modelBuilder.Entity<IdentityDocument>().HasKey(c => c.Id);

            // required

            // one-to-many
            modelBuilder.Entity<NaturalPerson>()
                .HasMany(s => s.IdentityDocuments)
                .WithOne(a => a.NaturalPerson);

        }


    }
}
