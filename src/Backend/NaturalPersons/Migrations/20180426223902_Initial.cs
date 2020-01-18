using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;

namespace NaturalPersons.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "naturalPersons");

            migrationBuilder.CreateTable(
                name: "NaturalPersons",
                schema: "naturalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalPersons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocuments",
                schema: "naturalPersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Authority = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfExpiration = table.Column<DateTime>(nullable: false),
                    DateOfIssue = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NaturalPersonId = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    RegistrationAddressId = table.Column<int>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Surname = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityDocuments_NaturalPersons_NaturalPersonId",
                        column: x => x.NaturalPersonId,
                        principalSchema: "naturalPersons",
                        principalTable: "NaturalPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityDocuments_NaturalPersonId",
                schema: "naturalPersons",
                table: "IdentityDocuments",
                column: "NaturalPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityDocuments",
                schema: "naturalPersons");

            migrationBuilder.DropTable(
                name: "NaturalPersons",
                schema: "naturalPersons");
        }
    }
}
