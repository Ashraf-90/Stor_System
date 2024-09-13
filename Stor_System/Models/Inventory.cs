using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            ProductsDetails = new HashSet<ProductsDetail>();
        }

        public int InventoryId { get; set; }
        public int? SLocationId { get; set; }
        public string InventoryName { get; set; } = null!;
        public string InventoryLocation { get; set; } = null!;
        public string? GMapLink { get; set; }
        public string? InventoryPhone { get; set; }

        public virtual ICollection<ProductsDetail> ProductsDetails { get; set; }
    }
}
