using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductsModels = new HashSet<ProductsModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<ProductsModel> ProductsModels { get; set; }
    }
}
