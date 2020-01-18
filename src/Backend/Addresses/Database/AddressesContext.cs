using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Addresses.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Addresses.Database
{
    public class AddressesContext : DbContext
    {

        public AddressesContext(DbContextOptions<AddressesContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<LocalityType> LocalityTypes { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<StreetType> StreetTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("addresses");

            // primary keys
            modelBuilder.Entity<Address>().HasKey(a => a.Id);
            modelBuilder.Entity<Country>().HasKey(c => c.Id);
            modelBuilder.Entity<District>().HasKey(d => d.Id);
            modelBuilder.Entity<Locality>().HasKey(l => l.Id);
            modelBuilder.Entity<LocalityType>().HasKey(lt => lt.Id);
            modelBuilder.Entity<Region>().HasKey(r => r.Id);
            modelBuilder.Entity<Street>().HasKey(s => s.Id);
            modelBuilder.Entity<StreetType>().HasKey(st => st.Id);

            // required
            modelBuilder.Entity<Country>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Country>().Property(c => c.Alpha2Code).IsRequired();
            modelBuilder.Entity<Country>().Property(c => c.Alpha3Code).IsRequired();
            modelBuilder.Entity<District>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Locality>().Property(l => l.Name).IsRequired();
            modelBuilder.Entity<Region>().Property(r => r.Name).IsRequired();
            modelBuilder.Entity<Street>().Property(s => s.Name).IsRequired();

            // data type
            modelBuilder.Entity<Country>()
                .Property(c => c.Alpha2Code).HasColumnType("char(2)");
            modelBuilder.Entity<Country>()
                .Property(c => c.Alpha3Code).HasColumnType("char(3)");

            // one-two-one
            modelBuilder.Entity<Street>()
                .HasOne(s => s.StreetType)
                .WithOne(st => st.Street)
                .HasForeignKey<Street>(st => st.StreetTypeId);

            modelBuilder.Entity<Locality>()
                .HasOne(l => l.LocalityType)
                .WithOne(lt => lt.Locality)
                .HasForeignKey<Locality>(lt => lt.LocalityTypeId);

            // one-to-many
            modelBuilder.Entity<Street>()
                .HasMany(s => s.Addresses)
                .WithOne(a => a.Street);

            modelBuilder.Entity<Region>()
                .HasMany(r => r.Localities)
                .WithOne(l => l.Region);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Localities)
                .WithOne(l => l.Country);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Regions)
                .WithOne(r => r.Country);

            modelBuilder.Entity<Locality>()
                .HasMany(l => l.Streets)
                .WithOne(s => s.Locality);

            modelBuilder.Entity<Locality>()
                .HasMany(l => l.Districts)
                .WithOne(d => d.Locality);

            modelBuilder.Entity<District>()
                .HasMany(d => d.Addresses)
                .WithOne(a => a.District);


        }

    }
}
