using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StockExportGenerator.Migrations
{
    public partial class AddedUniqueKeyToTemplateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemplateColumns_ExportTemplateId",
                table: "TemplateColumns");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateColumns_ExportTemplateId_Index",
                table: "TemplateColumns",
                columns: new[] { "ExportTemplateId", "Index" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemplateColumns_ExportTemplateId_Index",
                table: "TemplateColumns");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateColumns_ExportTemplateId",
                table: "TemplateColumns",
                column: "ExportTemplateId");
        }
    }
}
