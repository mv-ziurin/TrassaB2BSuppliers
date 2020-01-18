using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExportGenerator.Model
{
    public class StockExportGenContext : DbContext
    {
        public StockExportGenContext(DbContextOptions<StockExportGenContext> options) 
            : base(options)
        {
        }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<TemplateColumn> TemplateColumns { get; set; }

        public DbSet<ExportTemplate> ExportTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ExportTemplate>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(ExportTemplate.NAME_LENGTH);
            modelBuilder
                .Entity<ExportTemplate>()
                .Property(e => e.Format)
                .IsRequired()
                .HasMaxLength(ExportTemplate.FORMAT_LENGTH);
            modelBuilder
                .Entity<ExportTemplate>()
                .HasMany(e => e.TemplateColumns)
                .WithOne(e => e.ExportTemplate)
                .HasForeignKey(e => e.ExportTemplateId);
            modelBuilder
                .Entity<ExportTemplate>()
                .HasMany(e => e.Partners)
                .WithOne(e => e.ExportTemplate)
                .HasForeignKey(e => e.ExportTemplateId);

            modelBuilder
                .Entity<Partner>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(Partner.NAME_LENGTH);

            modelBuilder
                .Entity<TemplateColumn>()
                .HasIndex(nameof(TemplateColumn.ExportTemplateId), nameof(TemplateColumn.Index))
                .IsUnique();
            modelBuilder
                .Entity<TemplateColumn>()
                .Property(e => e.PimAttributeName)
                .IsRequired()
                .HasMaxLength(TemplateColumn.PIM_ATTR_LENGTH);
            modelBuilder
                .Entity<TemplateColumn>()
                .Property(e => e.PartnerAttributeName)
                .IsRequired()
                .HasMaxLength(TemplateColumn.PARTNER_ATTR_LENGTH);
        }
    }
}

