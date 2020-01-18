using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public enum SegmentType
    {
        Economy, 
        MidMinus,
        Mid,
        MidPlus,
        Premium,
        Laxury
    }

    public enum AreaType
    {
        Shop,
        DepartmentStore,
        Supermarket,
        Hypermarket
    }

    public enum FormatType
    {
        None,
        Corner,
        ShopInShop,
        FocusZone,
        Section
    }

    public class SalesPoint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AddressId { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Author { get; set; }

        public int Age { get; set; }

        public SegmentType TargetMarketSegment { get; set; }

        public AreaType Area { get; set; }

        public FormatType WorkingFormat { get; set; }

        public int CategoryId { get; set; }

        public int NumberOfSKU { get; set; }

        public int ChecksPerMonth { get; set; }

        public double AverageCheck { get; set; }

        public double AverageMarkUp { get; set; }

        public int CollaboratorNumber { get; set; }

        public double AverageBuyerTraffic { get; set; }

        public double SalesPerYear { get; set; }

        public double SalesWithTrassaPerYear { get; set; }

        public double TrassaPenetration { get; set; }

        public int PertnerId { get; set; }

        public virtual Partner Partner { get; set; }

        public virtual ICollection<SalesPointPhoto> SalesPointPhotos { get; set; }

        public virtual ICollection<PartnersContactDetail> PartnerContactDetails { get; set; }

        public SalesPoint()
        {
            this.SalesPointPhotos = new HashSet<SalesPointPhoto>();
            this.PartnerContactDetails = new HashSet<PartnersContactDetail>();
        }
    }
}
