using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public enum StatusType
    {
        Bronze,
        Silver,
        Gold
    }

    public enum NetType
    {
        Wholesale,
        Network
    }

    public class Partner
    {
        public int Id { get; set; }

        public int CounterpartyId { get; set; }

        public int AddressId { get; set; }

        public string OfficialSite { get; set; }

        public string Repetition { get; set; }

        public StatusType Vip { get; set; }

        public NetType Net { get; set; }

        public int OnlineSalesPercent { get; set; }

        public int DiscountGoodsPercent { get; set; }

        public int AssortmentPercent { get; set; }

        public string Specialization { get; set; }

        public string PriceSegment { get; set; }

        public int WriterId { get; set; }

        public DateTime CreationDate { get; set; }

        public int PurchasingDecisionModelId { get; set; }

        public virtual PurchasingDecisionModel PurchasingDecisionModel { get; set; }

        public virtual ICollection<SalesPoint> SalesPoints { get; set; }

        public virtual ICollection<PartnersPage> PartnersPages { get; set; }

        public virtual ICollection<PartnersContactDetail> PartnerContactDetails { get; set; }

        public Partner()
        {
            this.SalesPoints = new HashSet<SalesPoint>();
            this.PartnersPages = new HashSet<PartnersPage>();
            this.PartnerContactDetails = new HashSet<PartnersContactDetail>();
        }
    }
}
