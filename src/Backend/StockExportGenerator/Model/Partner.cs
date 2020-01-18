using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExportGenerator.Model
{
    public class Partner
    {
        public const int NAME_LENGTH = 256;

        public int Id { get; set; }

        /// <summary>
        /// Partner Id in Partners microservice
        /// </summary>
        public int PartnerId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// RRP(Recommended Retail Price) = Wholesale price * RrToWsPriceCoefficient
        /// </summary>
        public double RrToWsPriceCoefficient { get; set; }

        public int ExportTemplateId { get; set; }
        public virtual ExportTemplate ExportTemplate { get; set; }
    }
}
