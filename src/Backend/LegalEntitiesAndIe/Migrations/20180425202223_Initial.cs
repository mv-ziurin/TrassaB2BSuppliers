using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LegalEntitiesAndIe.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LegalEntitiesAndIe");

            migrationBuilder.CreateTable(
                name: "IndividualEntrepreneurs",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NaturalPersonId = table.Column<int>(nullable: false),
                    Ogrnip = table.Column<long>(nullable: false),
                    Inn = table.Column<long>(nullable: false),
                    EntryDateInOgrnip = table.Column<DateTime>(nullable: false),
                    TerminationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualEntrepreneurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntities",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AssignmentDateOfOgrn = table.Column<DateTime>(nullable: false),
                    TerminationDate = table.Column<DateTime>(nullable: false),
                    Head = table.Column<string>(nullable: true),
                    Founder = table.Column<string>(nullable: true),
                    LegalAddressId = table.Column<int>(nullable: false),
                    PostAddressId = table.Column<int>(nullable: false),
                    CentralOfficeAddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IeAccountingInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Inn = table.Column<long>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    TaxAuthorityName = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrip = table.Column<DateTime>(nullable: false),
                    RegistrationAuthorityName = table.Column<string>(nullable: true),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeAccountingInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeAccountingInfos_IndividualEntrepreneurs_IndividualEntrepr~",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IeAuthorityInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RegistrationAuthorityName = table.Column<string>(nullable: true),
                    RegistrationAuthorityId = table.Column<int>(nullable: false),
                    Patronymic = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateIntEgrip = table.Column<DateTime>(nullable: false),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeAuthorityInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeAuthorityInfos_IndividualEntrepreneurs_IndividualEntrepre~",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IeFullNames",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Surname = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateIntEgrip = table.Column<DateTime>(nullable: false),
                    RegistrationAuthorityName = table.Column<string>(nullable: true),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeFullNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeFullNames_IndividualEntrepreneurs_IndividualEntrepreneurId",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IeNationalities",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nationality = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrip = table.Column<DateTime>(nullable: false),
                    RegistrationAuthorityName = table.Column<string>(nullable: true),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeNationalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeNationalities_IndividualEntrepreneurs_IndividualEntrepren~",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IeRegistrationInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ogrnip = table.Column<long>(nullable: false),
                    EntryDateInOgrnip = table.Column<DateTime>(nullable: false),
                    OldRegistrationNumber = table.Column<long>(nullable: false),
                    OldRegistrationDate = table.Column<DateTime>(nullable: false),
                    OldRegistrationAuthority = table.Column<string>(nullable: true),
                    ReistrationAuthorityName = table.Column<string>(nullable: true),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeRegistrationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeRegistrationInfos_IndividualEntrepreneurs_IndividualEntre~",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IeTerminationInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TerminationMethod = table.Column<string>(nullable: true),
                    TerminationDate = table.Column<DateTime>(nullable: false),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrip = table.Column<DateTime>(nullable: false),
                    RegistrationAuthorityName = table.Column<string>(nullable: true),
                    IndividualEntrepreneurId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IeTerminationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IeTerminationInfos_IndividualEntrepreneurs_IndividualEntrep~",
                        column: x => x.IndividualEntrepreneurId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "IndividualEntrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeAccountingInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Inn = table.Column<long>(nullable: false),
                    Kpp = table.Column<long>(nullable: false),
                    RegistationDate = table.Column<DateTime>(nullable: false),
                    TaxAuthorityName = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrul = table.Column<DateTime>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeAccountingInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeAccountingInfos_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeAuthorityInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddressId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrul = table.Column<DateTime>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeAuthorityInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeAuthorityInfos_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeLocationAddresses",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AddressesId = table.Column<int>(nullable: false),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrul = table.Column<DateTime>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeLocationAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeLocationAddresses_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeNames",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrul = table.Column<DateTime>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeNames_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeRegistrationInfos",
                schema: "LegalEntitiesAndIe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Ogrn = table.Column<long>(nullable: false),
                    AssignmentDateOfOgrn = table.Column<DateTime>(nullable: false),
                    OldRegistrationNumber = table.Column<long>(nullable: false),
                    OldRegistrationAuthority = table.Column<string>(nullable: true),
                    Grn = table.Column<long>(nullable: false),
                    EntryDateInEgrul = table.Column<DateTime>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeRegistrationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeRegistrationInfos_LegalEntities_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalSchema: "LegalEntitiesAndIe",
                        principalTable: "LegalEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IeAccountingInfos_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeAccountingInfos",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IeAuthorityInfos_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeAuthorityInfos",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IeFullNames_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeFullNames",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IeNationalities_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeNationalities",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IeRegistrationInfos_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeRegistrationInfos",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IeTerminationInfos_IndividualEntrepreneurId",
                schema: "LegalEntitiesAndIe",
                table: "IeTerminationInfos",
                column: "IndividualEntrepreneurId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeAccountingInfos_LegalEntityId",
                schema: "LegalEntitiesAndIe",
                table: "LeAccountingInfos",
                column: "LegalEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeAuthorityInfos_LegalEntityId",
                schema: "LegalEntitiesAndIe",
                table: "LeAuthorityInfos",
                column: "LegalEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeLocationAddresses_LegalEntityId",
                schema: "LegalEntitiesAndIe",
                table: "LeLocationAddresses",
                column: "LegalEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeNames_LegalEntityId",
                schema: "LegalEntitiesAndIe",
                table: "LeNames",
                column: "LegalEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeRegistrationInfos_LegalEntityId",
                schema: "LegalEntitiesAndIe",
                table: "LeRegistrationInfos",
                column: "LegalEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IeAccountingInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IeAuthorityInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IeFullNames",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IeNationalities",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IeRegistrationInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IeTerminationInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LeAccountingInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LeAuthorityInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LeLocationAddresses",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LeNames",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LeRegistrationInfos",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "IndividualEntrepreneurs",
                schema: "LegalEntitiesAndIe");

            migrationBuilder.DropTable(
                name: "LegalEntities",
                schema: "LegalEntitiesAndIe");
        }
    }
}
