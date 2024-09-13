using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Brand
    {
        public Brand()
        {
            ProductsModels = new HashSet<ProductsModel>();
        }

        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;

        public virtual ICollection<ProductsModel> ProductsModels { get; set; }
    }
}
