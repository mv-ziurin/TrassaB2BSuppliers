using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Partners.Database.Models;

namespace Partners.Database
{
    public class PartnersContext : DbContext
    {
        public PartnersContext(DbContextOptions<PartnersContext> options) : base(options) { }

        public DbSet<File> Files { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<PartnersContactDetail> PartnerContactDetails { get; set; }

        public DbSet<PartnersPage> PartnersPages { get; set; }

        public DbSet<PurchasingDecisionModel> PurchasingDecisionModels { get; set; }

        public DbSet<SalesPoint> SalesPoints { get; set; }

        public DbSet<SalesPointPhoto> SalesPointPhotos { get; set; }

        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("partners");

            // primary keys
            modelBuilder.Entity<File>().HasKey(f => f.Id);
            modelBuilder.Entity<Partner>().HasKey(p => p.Id);
            modelBuilder.Entity<PartnersContactDetail>().HasKey(p => p.Id);
            modelBuilder.Entity<PartnersPage>().HasKey(p => p.Id);
            modelBuilder.Entity<PurchasingDecisionModel>().HasKey(p => p.Id);
            modelBuilder.Entity<SalesPoint>().HasKey(s => s.Id);
            modelBuilder.Entity<SalesPointPhoto>().HasKey(s => s.Id);
            modelBuilder.Entity<SocialNetwork>().HasKey(s => s.Id);

            // one-two-one
            modelBuilder.Entity<SalesPointPhoto>()
                .HasOne(s => s.File)
                .WithOne(f => f.SalesPointPhoto)
                .HasForeignKey<SalesPointPhoto>(s => s.FileId);

            // one-to-many
            modelBuilder.Entity<Partner>()
                .HasMany(p => p.SalesPoints)
                .WithOne(s => s.Partner);

            modelBuilder.Entity<Partner>()
                .HasMany(p => p.PartnersPages)
                .WithOne(p => p.Partner);

            modelBuilder.Entity<Partner>()
                .HasMany(p => p.PartnerContactDetails)
                .WithOne(p => p.Partner);

            modelBuilder.Entity<PurchasingDecisionModel>()
                .HasMany(p => p.Partners)
                .WithOne(p => p.PurchasingDecisionModel);

            modelBuilder.Entity<SalesPoint>()
                .HasMany(s => s.PartnerContactDetails)
                .WithOne(p => p.SalesPoint);

            modelBuilder.Entity<SalesPoint>()
                 .HasMany(s => s.SalesPointPhotos)
                 .WithOne(s => s.SalesPoint);

            modelBuilder.Entity<SocialNetwork>()
                 .HasMany(s => s.PartnersPages)
                 .WithOne(p => p.SocialNetwork);
        }
    }
}
