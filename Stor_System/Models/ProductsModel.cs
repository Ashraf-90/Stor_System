using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stor_System.Models
{
    public partial class ProductsModel
    {
        public ProductsModel()
        {
            ProductsDetails = new HashSet<ProductsDetail>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string ProductModel { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string? ImagePath { get; set; }
        public string? Active { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ProductsDetail> ProductsDetails { get; set; }

        [NotMapped]
        public int Quantity { get; set; }
        [NotMapped]
        public int QStatus { get; set; }
        [NotMapped]
        public int QMovnent { get; set; }
        [NotMapped]
        public int Avilable { get; set; }

        [NotMapped]
        public int PJUsered { get; set; }

    }
}
