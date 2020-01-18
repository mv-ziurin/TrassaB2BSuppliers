using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExportGenerator.Model
{
    public class ExportTemplate
    {
        public const int NAME_LENGTH = 256;
        public const int FORMAT_LENGTH = 32;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Format { get; set; }

        public bool IsActive { get; set; }

        public virtual List<TemplateColumn> TemplateColumns { get; set; }

        public virtual List<Partner> Partners { get; set; }
    }
}
