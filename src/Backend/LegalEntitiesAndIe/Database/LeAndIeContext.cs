using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LegalEntitiesAndIe.Database.Models;

namespace LegalEntitiesAndIe.Database
{
    public class LeAndIeContext : DbContext
    {
        public LeAndIeContext(DbContextOptions<LeAndIeContext> options) : base(options) { }

        public DbSet<IeAccountingInfo> IeAccountingInfos { get; set; }

        public DbSet<IeAuthorityInfo> IeAuthorityInfos { get; set; }

        public DbSet<IeFullName> IeFullNames { get; set; }

        public DbSet<IeNationality> IeNationalities { get; set; }

        public DbSet<IeRegistrationInfo> IeRegistrationInfos { get; set; }

        public DbSet<IeTerminationInfo> IeTerminationInfos { get; set; }

        public DbSet<IndividualEntrepreneur> IndividualEntrepreneurs { get; set; }

        public DbSet<LeAccountingInfo> LeAccountingInfos { get; set; }

        public DbSet<LeAuthorityInfo> LeAuthorityInfos { get; set; }

        public DbSet<LegalEntity> LegalEntities { get; set; }

        public DbSet<LeLocationAddress> LeLocationAddresses { get; set; }

        public DbSet<LeName> LeNames { get; set; }

        public DbSet<LeRegistrationInfo> LeRegistrationInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("LegalEntitiesAndIe");

            // primary keys
            modelBuilder.Entity<IeAccountingInfo>().HasKey(i => i.Id);
            modelBuilder.Entity<IeAuthorityInfo>().HasKey(i => i.Id);
            modelBuilder.Entity<IeFullName>().HasKey(i => i.Id);
            modelBuilder.Entity<IeNationality>().HasKey(i => i.Id);
            modelBuilder.Entity<IeRegistrationInfo>().HasKey(i => i.Id);
            modelBuilder.Entity<IeTerminationInfo>().HasKey(i => i.Id);
            modelBuilder.Entity<IndividualEntrepreneur>().HasKey(i => i.Id);
            modelBuilder.Entity<LeAccountingInfo>().HasKey(l => l.Id);
            modelBuilder.Entity<LeAuthorityInfo>().HasKey(l => l.Id);
            modelBuilder.Entity<LegalEntity>().HasKey(l => l.Id);
            modelBuilder.Entity<LeLocationAddress>().HasKey(l => l.Id);
            modelBuilder.Entity<LeName>().HasKey(l => l.Id);
            modelBuilder.Entity<LeRegistrationInfo>().HasKey(l => l.Id);

            // one-two-one
            modelBuilder.Entity<LegalEntity>()
                .HasOne(l => l.LeName)
                .WithOne(l => l.LegalEntity)
                .HasForeignKey<LeName>(l => l.LegalEntityId);

            modelBuilder.Entity<LegalEntity>()
                .HasOne(l => l.LeLocationAddress)
                .WithOne(l => l.LegalEntity)
                .HasForeignKey<LeLocationAddress>(l => l.LegalEntityId);

            modelBuilder.Entity<LegalEntity>()
                .HasOne(l => l.LeRegistrationInfo)
                .WithOne(l => l.LegalEntity)
                .HasForeignKey<LeRegistrationInfo>(l => l.LegalEntityId);

            modelBuilder.Entity<LegalEntity>()
                .HasOne(l => l.LeAccountingInfo)
                .WithOne(l => l.LegalEntity)
                .HasForeignKey<LeAccountingInfo>(l => l.LegalEntityId);

            modelBuilder.Entity<LegalEntity>()
                .HasOne(l => l.LeAuthorityInfo)
                .WithOne(l => l.LegalEntity)
                .HasForeignKey<LeAuthorityInfo>(l => l.LegalEntityId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeAccountingInfo)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeAccountingInfo>(i => i.IndividualEntrepreneurId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeAuthorityInfo)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeAuthorityInfo>(i => i.IndividualEntrepreneurId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeFullName)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeFullName>(i => i.IndividualEntrepreneurId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeNationality)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeNationality>(i => i.IndividualEntrepreneurId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeRegistrationInfo)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeRegistrationInfo>(i => i.IndividualEntrepreneurId);

            modelBuilder.Entity<IndividualEntrepreneur>()
                .HasOne(i => i.IeTerminationInfo)
                .WithOne(i => i.IndividualEntrepreneur)
                .HasForeignKey<IeTerminationInfo>(i => i.IndividualEntrepreneurId);

        }
    }
}
