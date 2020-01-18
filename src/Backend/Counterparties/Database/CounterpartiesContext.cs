using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Counterparties.Database.Models;

namespace Counterparties.Database
{
    public class CounterpartiesContext : DbContext
    {
        public CounterpartiesContext(DbContextOptions<CounterpartiesContext> options) : base(options) { }

        public DbSet<Counterparty> Counterparties { get; set; }

        public DbSet<CounterpartyGroupOwner> CounterpartyGroupOwners { get; set; }

        public DbSet<Filial> Filials { get; set; }

        public DbSet<FilialCollaborator> FilialCollaborators { get; set; }

        public DbSet<FilialPhoto> FilialPhotos { get; set; }

        public DbSet<GroupName> GroupNames { get; set; }

        public DbSet<GroupOwner> GroupOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("counterparties");

            // primary keys
            modelBuilder.Entity<Counterparty>().HasKey(c => c.Id);
            modelBuilder.Entity<CounterpartyGroupOwner>().HasKey(c => c.Id);
            modelBuilder.Entity<Filial>().HasKey(f => f.Id);
            modelBuilder.Entity<FilialCollaborator>().HasKey(f => f.Id);
            modelBuilder.Entity<FilialPhoto>().HasKey(f => f.Id);
            modelBuilder.Entity<GroupName>().HasKey(g => g.Id);
            modelBuilder.Entity<GroupOwner>().HasKey(g => g.Id);

            // one-to-many
            modelBuilder.Entity<GroupName>()
                .HasMany(g => g.Counterparties)
                .WithOne(c => c.GroupName);

            modelBuilder.Entity<Counterparty>()
                .HasMany(c => c.Filials)
                .WithOne(f => f.Counterparty);

            modelBuilder.Entity<Filial>()
                .HasMany(f => f.FilialCollaborators)
                .WithOne(f => f.Filial);

            modelBuilder.Entity<Filial>()
               .HasMany(f => f.FilialPhotos)
               .WithOne(f => f.Filial);

            // many-to-many
            modelBuilder.Entity<CounterpartyGroupOwner>()
                .HasOne(c => c.Counterparty)
                .WithMany(c => c.CounterpartyGroupOwners)
               .HasForeignKey(c => c.CounterpartyId);

            modelBuilder.Entity<CounterpartyGroupOwner>()
                .HasOne(c=> c.GroupOwner)
                .WithMany(g => g.CounterpartyGroupOwners)
                .HasForeignKey(c => c.GroupOwnerId);

        }

    }
}
