using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Suppliers.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Suppliers");

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    CounteragentId = table.Column<int>(nullable: false),
                    DistributionDealSide = table.Column<int>(nullable: false),
                    DistributionModelRU = table.Column<int>(nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(nullable: false),
                    SupplyDealSide = table.Column<int>(nullable: false),
                    TargetMarketSegmentRU = table.Column<int>(nullable: false),
                    YearOfDistributionRU = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandCompetitors",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    CompetitorBrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandCompetitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandCompetitors_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandCompetitors_Brands_CompetitorBrandId",
                        column: x => x.CompetitorBrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandConsumerProfilesRU",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    ConsumerProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandConsumerProfilesRU", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandConsumerProfilesRU_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandContacts",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandContacts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandRecognitionRecords",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    ConsumerProfileId = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    NumberOfNegativeResponds = table.Column<int>(nullable: false),
                    NumberOfNeutralResponds = table.Column<int>(nullable: false),
                    NumberOfPositiveResponds = table.Column<int>(nullable: false),
                    QualityQuestion = table.Column<string>(nullable: false),
                    Respondent = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandRecognitionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandRecognitionRecords_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandSalesChannels",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    TargetMarketSegment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSalesChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandSalesChannels_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonsByChannelRecognition",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    ChannelCoverageIndex = table.Column<int>(nullable: false),
                    ChannelType = table.Column<string>(nullable: true),
                    CompetitorBrandId = table.Column<int>(nullable: false),
                    NumberOfChannels = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonsByChannelRecognition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonsByChannelRecognition_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonsByChannelRecognition_Brands_CompetitorBrandId",
                        column: x => x.CompetitorBrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonsByPopularity",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    CompetitorBrandId = table.Column<int>(nullable: false),
                    ConsumerProfileId = table.Column<int>(nullable: false),
                    PopularityScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonsByPopularity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonsByPopularity_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonsByPopularity_Brands_CompetitorBrandId",
                        column: x => x.CompetitorBrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonsByPrice",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AveragePrice = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    CompetitorBrandId = table.Column<int>(nullable: false),
                    PriceCompetitivenessIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonsByPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonsByPrice_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonsByPrice_Brands_CompetitorBrandId",
                        column: x => x.CompetitorBrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonsByRangeSize",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    CompetitorBrandId = table.Column<int>(nullable: false),
                    NumberOfSKU = table.Column<int>(nullable: false),
                    RangeSizeIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonsByRangeSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonsByRangeSize_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonsByRangeSize_Brands_CompetitorBrandId",
                        column: x => x.CompetitorBrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetPromoterScores",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    ConsumerProfileId = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    NumberOfNegativeResponds = table.Column<int>(nullable: false),
                    NumberOfNeutralResponds = table.Column<int>(nullable: false),
                    NumberOfPositiveResponds = table.Column<int>(nullable: false),
                    RecomendationQuestion = table.Column<string>(nullable: false),
                    Respondent = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetPromoterScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetPromoterScores_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PotentialMarketsRU",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AnnualPurchaseFrequency = table.Column<int>(nullable: false),
                    AveragePurchaseSize = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryTreeId = table.Column<int>(nullable: false),
                    ConsumerProfileId = table.Column<int>(nullable: false),
                    NumberOfPotentialConsumers = table.Column<int>(nullable: false),
                    PotentialAnnualMarketRU = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotentialMarketsRU", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotentialMarketsRU_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<int>(nullable: false),
                    DateOfB2BCatalogFilesAcquisiton = table.Column<DateTime>(nullable: false),
                    DateOfB2BCollectionAndPricePublishing = table.Column<DateTime>(nullable: false),
                    DateOfB2BPreorderCampaignInvitations = table.Column<DateTime>(nullable: false),
                    DateOfBrandStatisticsAcquisition = table.Column<DateTime>(nullable: false),
                    DateOfCatalogAcquisition = table.Column<DateTime>(nullable: false),
                    DateOfCollectionShootingEnd = table.Column<DateTime>(nullable: false),
                    DateOfMoscowShowroomPrep = table.Column<DateTime>(nullable: false),
                    DateOfPrepForPartnerBuyersTrainingActivities = table.Column<DateTime>(nullable: false),
                    DateOfPresentationPrep = table.Column<DateTime>(nullable: false),
                    DateOfProducerPhotoAcquisiton = table.Column<DateTime>(nullable: false),
                    DateOfProducerSampleDispatch = table.Column<DateTime>(nullable: false),
                    DateOfProducerSampleReceipt = table.Column<DateTime>(nullable: false),
                    DateOfRegionalSampleDispatch = table.Column<DateTime>(nullable: false),
                    DateOfRegionalSampleReceipt = table.Column<DateTime>(nullable: false),
                    DateOfShowroomPhoneInvitations = table.Column<DateTime>(nullable: false),
                    SeasonType = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Suppliers",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewCollectionPresentations",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateOfPerformance = table.Column<DateTime>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    Venue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewCollectionPresentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewCollectionPresentations_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "Suppliers",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonDeadlines",
                schema: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateOfCurrentDeadlineBrandManagerPreorderCheck = table.Column<DateTime>(nullable: false),
                    DateOfCurrentDeadlineDeliveryReadyness = table.Column<DateTime>(nullable: false),
                    DateOfCurrentDeadlineOrderPost = table.Column<DateTime>(nullable: false),
                    DateOfCurrentDeadlineOrderProductCommiteeReconciliation = table.Column<DateTime>(nullable: false),
                    DateOfCurrentDeadlineWarehouseOrderReceipt = table.Column<DateTime>(nullable: false),
                    DateOfCurrentDeadlineWarehouseProgramOrderOffer = table.Column<DateTime>(nullable: false),
                    DateOfPartnerPreorderDeadline = table.Column<DateTime>(nullable: false),
                    DeadlineNumber = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonDeadlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonDeadlines_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalSchema: "Suppliers",
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandCompetitors_BrandId",
                schema: "Suppliers",
                table: "BrandCompetitors",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandCompetitors_CompetitorBrandId",
                schema: "Suppliers",
                table: "BrandCompetitors",
                column: "CompetitorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandConsumerProfilesRU_BrandId",
                schema: "Suppliers",
                table: "BrandConsumerProfilesRU",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandContacts_BrandId",
                schema: "Suppliers",
                table: "BrandContacts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandRecognitionRecords_BrandId",
                schema: "Suppliers",
                table: "BrandRecognitionRecords",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSalesChannels_BrandId",
                schema: "Suppliers",
                table: "BrandSalesChannels",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByChannelRecognition_BrandId",
                schema: "Suppliers",
                table: "ComparisonsByChannelRecognition",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByChannelRecognition_CompetitorBrandId",
                schema: "Suppliers",
                table: "ComparisonsByChannelRecognition",
                column: "CompetitorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByPopularity_BrandId",
                schema: "Suppliers",
                table: "ComparisonsByPopularity",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByPopularity_CompetitorBrandId",
                schema: "Suppliers",
                table: "ComparisonsByPopularity",
                column: "CompetitorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByPrice_BrandId",
                schema: "Suppliers",
                table: "ComparisonsByPrice",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByPrice_CompetitorBrandId",
                schema: "Suppliers",
                table: "ComparisonsByPrice",
                column: "CompetitorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByRangeSize_BrandId",
                schema: "Suppliers",
                table: "ComparisonsByRangeSize",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonsByRangeSize_CompetitorBrandId",
                schema: "Suppliers",
                table: "ComparisonsByRangeSize",
                column: "CompetitorBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_NetPromoterScores_BrandId",
                schema: "Suppliers",
                table: "NetPromoterScores",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_NewCollectionPresentations_SeasonId",
                schema: "Suppliers",
                table: "NewCollectionPresentations",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PotentialMarketsRU_BrandId",
                schema: "Suppliers",
                table: "PotentialMarketsRU",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonDeadlines_SeasonId",
                schema: "Suppliers",
                table: "SeasonDeadlines",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_BrandId",
                schema: "Suppliers",
                table: "Seasons",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandCompetitors",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "BrandConsumerProfilesRU",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "BrandContacts",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "BrandRecognitionRecords",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "BrandSalesChannels",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "ComparisonsByChannelRecognition",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "ComparisonsByPopularity",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "ComparisonsByPrice",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "ComparisonsByRangeSize",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "NetPromoterScores",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "NewCollectionPresentations",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "PotentialMarketsRU",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "SeasonDeadlines",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "Seasons",
                schema: "Suppliers");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "Suppliers");
        }
    }
}
