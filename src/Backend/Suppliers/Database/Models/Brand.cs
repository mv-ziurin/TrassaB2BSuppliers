using System.Collections.Generic;

namespace Suppliers.Database.Models
{
    public class Brand
    {
        public enum DistributionModelType
        {
            ExclusiveDistributor,
            Dealer,
            StraightDistribution,
            Agent,
            Commissionaire,
            RepresentativeOffice
        }

        public enum DealSideType
        {
            LegalPerson,
            Counteragent,
            Supplier
        }

        public int Id { get; set; }

        public int CounteragentId { get; set; }

        public int ResponsibleEmployeeId { get; set; }

        public int TargetMarketSegmentRU { get; set; }

        public int YearOfDistributionRU { get; set; }

        public DistributionModelType DistributionModelRU { get; set; } 

        public int CategoryTreeId { get; set; } //FK Category tree

        public DealSideType DistributionDealSide { get; set; }

        public DealSideType SupplyDealSide { get; set; }

        public virtual ICollection<PotentialMarketRU> PotentialMarketsRU { get; set; }

        public virtual ICollection<BrandSalesChannel> BrandSalesChannels { get; set; }

        public virtual ICollection<BrandCompetitor> BrandCompetitors { get; set; }

        public virtual ICollection<BrandCompetitor> Brands { get; set; }

        public virtual ICollection<BrandContact> BrandContacts { get; set; }

        public virtual ICollection<BrandRecognition> BrandRecognitionRecords { get; set; }

        public virtual ICollection<NetPromoterScore> NetPromoterScores { get; set; }

        public virtual ICollection<BrandConsumerProfileRU> BrandConsumerProfilesRU { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }

        public virtual ICollection<ComparisonByRangeSize> ComparisonsByRange { get; set; }

        public virtual ICollection<ComparisonByRangeSize> BrandsComparedByRange { get; set; }

        public virtual ICollection<ComparisonByPopularity> ComparisonsByPopularity { get; set; }

        public virtual ICollection<ComparisonByPopularity> BrandsComparedByPopularity { get; set; }

        public virtual ICollection<ComparisonByChannelRecognition>
                                                    ComparisonsByChannelRecognition { get; set; }

        public virtual ICollection<ComparisonByChannelRecognition> BrandsComparedByChannelRecognition { get; set; }

        public virtual ICollection<ComparisonByPrice> ComparisonsByPrice { get; set; }

        public virtual ICollection<ComparisonByPrice> BrandsComparedByPrice { get; set; }

        public Brand()
        {
            this.PotentialMarketsRU = new HashSet<PotentialMarketRU>();
            this.BrandSalesChannels = new HashSet<BrandSalesChannel>();
            this.BrandCompetitors = new HashSet<BrandCompetitor>();
            this.Brands = new HashSet<BrandCompetitor>();
            this.BrandContacts = new HashSet<BrandContact>();
            this.BrandRecognitionRecords = new HashSet<BrandRecognition>();
            this.NetPromoterScores = new HashSet<NetPromoterScore>();
            this.BrandConsumerProfilesRU = new HashSet<BrandConsumerProfileRU>();
            this.Seasons = new HashSet<Season>();
            this.ComparisonsByRange = new HashSet<ComparisonByRangeSize>();
            this.BrandsComparedByRange = new HashSet<ComparisonByRangeSize>();
            this.ComparisonsByPopularity = new HashSet<ComparisonByPopularity>();
            this.BrandsComparedByPopularity = new HashSet<ComparisonByPopularity>();
            this.ComparisonsByChannelRecognition = new HashSet<ComparisonByChannelRecognition>();
            this.BrandsComparedByChannelRecognition = new HashSet<ComparisonByChannelRecognition>();
            this.ComparisonsByPrice = new HashSet<ComparisonByPrice>();
            this.BrandsComparedByPrice = new HashSet<ComparisonByPrice>();
        }

    }
}
