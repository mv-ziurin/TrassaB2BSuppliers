using LegalWorkers.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace LegalWorkers.Database
{
    public class LegalWorkersContext : DbContext
    {

        public LegalWorkersContext(DbContextOptions<LegalWorkersContext> options) : base(options) { }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<WorkerFunction> WorkerFunctions { get; set; }

        public DbSet<EmployeeFunction> EmployeeFunctions { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UQPDSection> UQPDSections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("legalWorkers");

            // public keys
            modelBuilder.Entity<Worker>().HasKey(a => a.Id);
            modelBuilder.Entity<WorkerFunction>().HasKey(c => c.Id);
            modelBuilder.Entity<EmployeeFunction>().HasKey(a => a.Id);
            modelBuilder.Entity<Post>().HasKey(a => a.Id);
            modelBuilder.Entity<UQPDSection>().HasKey(a => a.Id);

            // required
            modelBuilder.Entity<WorkerFunction>().Property(a => a.Comment).IsRequired();
            modelBuilder.Entity<WorkerFunction>().Property(a => a.DateOfRecordCreation).IsRequired();
            modelBuilder.Entity<Post>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Post>().Property(a => a.Description).IsRequired();
            modelBuilder.Entity<EmployeeFunction>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<EmployeeFunction>().Property(a => a.Description).IsRequired();
            modelBuilder.Entity<UQPDSection>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<UQPDSection>().Property(a => a.Description).IsRequired();

            // one-to-many
            modelBuilder.Entity<Worker>()
                .HasMany(s => s.Functions)
                .WithOne(a => a.Worker);

            modelBuilder.Entity<Worker>()
                .HasMany(s => s.Workers)
                .WithOne(a => a.Director);

            modelBuilder.Entity<Post>()
                .HasMany(s => s.Workers)
                .WithOne(a => a.Post);

            modelBuilder.Entity<UQPDSection>()
                .HasMany(s => s.Posts)
                .WithOne(a => a.Section);

            modelBuilder.Entity<EmployeeFunction>()
                .HasMany(s => s.Functions)
                .WithOne(a => a.Function);
        }


    }
}
