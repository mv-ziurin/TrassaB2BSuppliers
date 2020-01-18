using Microsoft.EntityFrameworkCore;
using Suppliers.Database.Models;

namespace Suppliers.Database
{
    public class SuppliersContext : DbContext
    {

        public SuppliersContext(DbContextOptions<SuppliersContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<BrandCompetitor> BrandCompetitors { get; set; }

        public DbSet<BrandConsumerProfileRU> BrandConsumerProfilesRU { get; set; }

        public DbSet<BrandRecognition> BrandRecognitionRecords { get; set; }

        public DbSet<BrandSalesChannel> BrandSalesChannels { get; set; }

        public DbSet<BrandContact> BrandContacts { get; set; }

        public DbSet<ComparisonByChannelRecognition> ComparisonsByChannelRecognition { get; set; }

        public DbSet<ComparisonByPopularity> ComparisonsByPopularity { get; set; }

        public DbSet<ComparisonByPrice> ComparisonsByPrice { get; set; }

        public DbSet<ComparisonByRangeSize> ComparisonsByRange { get; set; }

        public DbSet<NetPromoterScore> NetPromoterScores { get; set; }

        public DbSet<PotentialMarketRU> PotentialMarketsRU { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<NewCollectionPresentation> NewCollectionPresentations { get; set; }

        public DbSet<SeasonDeadline> SeasonDeadlines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Suppliers");

            // public keys
            modelBuilder.Entity<Brand>().HasKey(a => a.Id);
            modelBuilder.Entity<BrandCompetitor>().HasKey(a => a.Id);
            modelBuilder.Entity<BrandConsumerProfileRU>().HasKey(a => a.Id);
            modelBuilder.Entity<BrandRecognition>().HasKey(a => a.Id);
            modelBuilder.Entity<BrandSalesChannel>().HasKey(a => a.Id);
            modelBuilder.Entity<BrandContact>().HasKey(a => a.Id);
            modelBuilder.Entity<ComparisonByChannelRecognition>().HasKey(a => a.Id);
            modelBuilder.Entity<ComparisonByPopularity>().HasKey(a => a.Id);
            modelBuilder.Entity<ComparisonByPrice>().HasKey(a => a.Id);
            modelBuilder.Entity<ComparisonByRangeSize>().HasKey(a => a.Id);
            modelBuilder.Entity<NetPromoterScore>().HasKey(a => a.Id);
            modelBuilder.Entity<NewCollectionPresentation>().HasKey(a => a.Id);
            modelBuilder.Entity<PotentialMarketRU>().HasKey(a => a.Id);
            modelBuilder.Entity<Season>().HasKey(a => a.Id);
            modelBuilder.Entity<SeasonDeadline>().HasKey(a => a.Id);

            // required
            modelBuilder.Entity<Brand>().Property(c => c.CounteragentId).IsRequired();
            modelBuilder.Entity<Brand>().Property(c => c.DistributionDealSide).IsRequired();
            modelBuilder.Entity<Brand>().Property(c => c.SupplyDealSide).IsRequired();
            modelBuilder.Entity<BrandSalesChannel>().Property(c => c.TargetMarketSegment).IsRequired();
            modelBuilder.Entity<PotentialMarketRU>().Property(c => c.PotentialAnnualMarketRU).IsRequired();
            modelBuilder.Entity<BrandRecognition>().Property(c => c.Respondent).IsRequired();
            modelBuilder.Entity<BrandRecognition>().Property(c => c.QualityQuestion).IsRequired();
            modelBuilder.Entity<NetPromoterScore>().Property(c => c.Respondent).IsRequired();
            modelBuilder.Entity<NetPromoterScore>().Property(c => c.RecomendationQuestion).IsRequired();
            modelBuilder.Entity<ComparisonByChannelRecognition>().Property(c => c.ChannelCoverageIndex).IsRequired();
            modelBuilder.Entity<ComparisonByPopularity>().Property(c => c.PopularityScore).IsRequired();
            modelBuilder.Entity<ComparisonByPrice>().Property(c => c.PriceCompetitivenessIndex).IsRequired();
            modelBuilder.Entity<ComparisonByRangeSize>().Property(c => c.RangeSizeIndex).IsRequired();
            modelBuilder.Entity<Season>().Property(c => c.SeasonType).IsRequired();
            modelBuilder.Entity<Season>().Property(c => c.Year).IsRequired();
            modelBuilder.Entity<SeasonDeadline>().Property(c => c.DeadlineNumber).IsRequired();
            modelBuilder.Entity<NewCollectionPresentation>().Property(c => c.DateOfPerformance).IsRequired();

            // one-to-many
            modelBuilder.Entity<Brand>()
                .HasMany(s => s.PotentialMarketsRU)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandSalesChannels)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(b => b.BrandCompetitors)
                .WithOne(b => b.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Brands)
                .WithOne(b => b.CompetitorBrand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandConsumerProfilesRU)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandContacts)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandRecognitionRecords)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.NetPromoterScores)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.ComparisonsByChannelRecognition)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandsComparedByChannelRecognition)
                .WithOne(a => a.CompetitorBrand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.ComparisonsByPopularity)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandsComparedByPopularity)
                .WithOne(a => a.CompetitorBrand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.ComparisonsByPrice)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandsComparedByPrice)
                .WithOne(a => a.CompetitorBrand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.ComparisonsByRange)
                .WithOne(a => a.Brand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.BrandsComparedByRange)
                .WithOne(a => a.CompetitorBrand);

            modelBuilder.Entity<Brand>()
                .HasMany(s => s.Seasons)
                .WithOne(a => a.Brand);
            

            modelBuilder.Entity<Season>()
                .HasMany(s => s.NewCollectionPresentations)
                .WithOne(a => a.Season);

            modelBuilder.Entity<Season>()
                .HasMany(s => s.SeasonDeadlines)
                .WithOne(a => a.Season);

        }
    }
}