using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StockExportGenerator.Migrations
{
    public partial class AddRrToWsPriceCoefficientToPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RrToWsPriceCoefficient",
                table: "Partners",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RrToWsPriceCoefficient",
                table: "Partners");
        }
    }
}
