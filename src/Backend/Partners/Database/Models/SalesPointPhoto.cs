using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partners.Database.Models
{
    public enum PhotoType
    {
        ShopWindow,
        ShoppingRoom
    }

    public class SalesPointPhoto
    {
        public int Id { get; set; }

        public PhotoType Type { get; set; }

        public int SalesPointId { get; set; }
       
        public virtual SalesPoint SalesPoint { get; set; }

        public int FileId { get; set; }

        public virtual File File { get; set; }

    }
}
