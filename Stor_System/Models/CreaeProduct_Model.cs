using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class CreaeProduct_Model
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int InventoryId { get; set; }
        public string ProductModel { get; set; }
        public string ProductName { get; set; }
        public IFormFile? ImagePath { get; set; }
        public string? Active { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual Inventory Inventory { get; set; } = null!;
    }
}
