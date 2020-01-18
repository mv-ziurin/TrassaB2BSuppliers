using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExportGenerator.Model
{
    public class TemplateColumn
    {
        public const int PIM_ATTR_LENGTH = 256;
        public const int PARTNER_ATTR_LENGTH = 256;

        public int Id { get; set; }

        public string PimAttributeName { get; set; }

        public string PartnerAttributeName { get; set; }

        public int Index { get; set; }

        public int ExportTemplateId { get; set; }
        public virtual ExportTemplate ExportTemplate { get; set; }
    }
}
