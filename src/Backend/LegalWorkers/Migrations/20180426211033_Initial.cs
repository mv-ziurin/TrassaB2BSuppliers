using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;

namespace LegalWorkers.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "legalWorkers");

            migrationBuilder.CreateTable(
                name: "EmployeeFunctions",
                schema: "legalWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CounteragentType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UQPDSections",
                schema: "legalWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UQPDSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "legalWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_UQPDSections_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "legalWorkers",
                        principalTable: "UQPDSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                schema: "legalWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DepartmentId = table.Column<int>(nullable: false),
                    DirectorId = table.Column<int>(nullable: false),
                    LegalEntityId = table.Column<int>(nullable: false),
                    NaturalPersonId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Workers_DirectorId",
                        column: x => x.DirectorId,
                        principalSchema: "legalWorkers",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "legalWorkers",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerFunctions",
                schema: "legalWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    DateOfRecordCreation = table.Column<DateTime>(nullable: false),
                    DateOfRecordRemoval = table.Column<DateTime>(nullable: false),
                    FunctionId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerFunctions_EmployeeFunctions_FunctionId",
                        column: x => x.FunctionId,
                        principalSchema: "legalWorkers",
                        principalTable: "EmployeeFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerFunctions_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalSchema: "legalWorkers",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SectionId",
                schema: "legalWorkers",
                table: "Posts",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerFunctions_FunctionId",
                schema: "legalWorkers",
                table: "WorkerFunctions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerFunctions_WorkerId",
                schema: "legalWorkers",
                table: "WorkerFunctions",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_DirectorId",
                schema: "legalWorkers",
                table: "Workers",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PostId",
                schema: "legalWorkers",
                table: "Workers",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerFunctions",
                schema: "legalWorkers");

            migrationBuilder.DropTable(
                name: "EmployeeFunctions",
                schema: "legalWorkers");

            migrationBuilder.DropTable(
                name: "Workers",
                schema: "legalWorkers");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "legalWorkers");

            migrationBuilder.DropTable(
                name: "UQPDSections",
                schema: "legalWorkers");
        }
    }
}
