using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Counterparties.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "counterparties");

            migrationBuilder.CreateTable(
                name: "GroupNames",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupOwners",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NaturalPersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counterparties",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OrganizationId = table.Column<int>(nullable: false),
                    OrganizationForm = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    EmployeesNumber = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    GroupNameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counterparties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counterparties_GroupNames_GroupNameId",
                        column: x => x.GroupNameId,
                        principalSchema: "counterparties",
                        principalTable: "GroupNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterpartyGroupOwner",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CounterpartyId = table.Column<int>(nullable: false),
                    GroupOwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterpartyGroupOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CounterpartyGroupOwner_Counterparties_CounterpartyId",
                        column: x => x.CounterpartyId,
                        principalSchema: "counterparties",
                        principalTable: "Counterparties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterpartyGroupOwner_GroupOwners_GroupOwnerId",
                        column: x => x.GroupOwnerId,
                        principalSchema: "counterparties",
                        principalTable: "GroupOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filials",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CountepartyId = table.Column<int>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CounterpartyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filials_Counterparties_CounterpartyId",
                        column: x => x.CounterpartyId,
                        principalSchema: "counterparties",
                        principalTable: "Counterparties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilialCollaborators",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FilialId = table.Column<int>(nullable: false),
                    CollaboratorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilialCollaborators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilialCollaborators_Filials_FilialId",
                        column: x => x.FilialId,
                        principalSchema: "counterparties",
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilialPhotos",
                schema: "counterparties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FilialId = table.Column<int>(nullable: false),
                    FileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilialPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilialPhotos_Filials_FilialId",
                        column: x => x.FilialId,
                        principalSchema: "counterparties",
                        principalTable: "Filials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counterparties_GroupNameId",
                schema: "counterparties",
                table: "Counterparties",
                column: "GroupNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterpartyGroupOwner_CounterpartyId",
                schema: "counterparties",
                table: "CounterpartyGroupOwner",
                column: "CounterpartyId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterpartyGroupOwner_GroupOwnerId",
                schema: "counterparties",
                table: "CounterpartyGroupOwner",
                column: "GroupOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FilialCollaborators_FilialId",
                schema: "counterparties",
                table: "FilialCollaborators",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_FilialPhotos_FilialId",
                schema: "counterparties",
                table: "FilialPhotos",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_Filials_CounterpartyId",
                schema: "counterparties",
                table: "Filials",
                column: "CounterpartyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterpartyGroupOwner",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "FilialCollaborators",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "FilialPhotos",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "GroupOwners",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "Filials",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "Counterparties",
                schema: "counterparties");

            migrationBuilder.DropTable(
                name: "GroupNames",
                schema: "counterparties");
        }
    }
}
