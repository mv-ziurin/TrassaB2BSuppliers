using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Addresses.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "addresses");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Alpha2Code = table.Column<string>(type: "char(2)", nullable: false),
                    Alpha3Code = table.Column<string>(type: "char(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalityTypes",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StreetTypes",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "addresses",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    LocalityTypeId = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "addresses",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localities_LocalityTypes_LocalityTypeId",
                        column: x => x.LocalityTypeId,
                        principalSchema: "addresses",
                        principalTable: "LocalityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localities_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "addresses",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalSchema: "addresses",
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    StreetTypeId = table.Column<int>(nullable: false),
                    LocalityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalSchema: "addresses",
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Streets_StreetTypes_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalSchema: "addresses",
                        principalTable: "StreetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StreetId = table.Column<int>(nullable: false),
                    DistrictId = table.Column<int>(nullable: false),
                    House = table.Column<string>(nullable: true),
                    HouseBlock = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    HomeOwnership = table.Column<string>(nullable: true),
                    Ownership = table.Column<string>(nullable: true),
                    Apartment = table.Column<string>(nullable: true),
                    Postcode = table.Column<int>(nullable: false),
                    Entrance = table.Column<int>(nullable: false),
                    Intercom = table.Column<string>(nullable: true),
                    Floor = table.Column<int>(nullable: false),
                    Pavilion = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "addresses",
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Streets_StreetId",
                        column: x => x.StreetId,
                        principalSchema: "addresses",
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                schema: "addresses",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                schema: "addresses",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_LocalityId",
                schema: "addresses",
                table: "Districts",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_CountryId",
                schema: "addresses",
                table: "Localities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_LocalityTypeId",
                schema: "addresses",
                table: "Localities",
                column: "LocalityTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localities_RegionId",
                schema: "addresses",
                table: "Localities",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                schema: "addresses",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_LocalityId",
                schema: "addresses",
                table: "Streets",
                column: "LocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_StreetTypeId",
                schema: "addresses",
                table: "Streets",
                column: "StreetTypeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "Districts",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "Streets",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "Localities",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "StreetTypes",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "LocalityTypes",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "addresses");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "addresses");
        }
    }
}
