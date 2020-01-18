﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StockExportGenerator.Model;
using System;

namespace StockExportGenerator.Migrations
{
    [DbContext(typeof(StockExportGenContext))]
    [Migration("20180420171500_AddedUniqueKeyToTemplateColumn")]
    partial class AddedUniqueKeyToTemplateColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("StockExportGenerator.Model.ExportTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Format");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ExportTemplates");
                });

            modelBuilder.Entity("StockExportGenerator.Model.Partner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExportTemplateId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ExportTemplateId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("StockExportGenerator.Model.TemplateColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExportTemplateId");

                    b.Property<int>("Index");

                    b.Property<string>("PartnerAttributeName");

                    b.Property<string>("PimAttributeName");

                    b.HasKey("Id");

                    b.HasIndex("ExportTemplateId", "Index")
                        .IsUnique();

                    b.ToTable("TemplateColumns");
                });

            modelBuilder.Entity("StockExportGenerator.Model.Partner", b =>
                {
                    b.HasOne("StockExportGenerator.Model.ExportTemplate", "ExportTemplate")
                        .WithMany("Partners")
                        .HasForeignKey("ExportTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StockExportGenerator.Model.TemplateColumn", b =>
                {
                    b.HasOne("StockExportGenerator.Model.ExportTemplate", "ExportTemplate")
                        .WithMany("TemplateColumns")
                        .HasForeignKey("ExportTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}