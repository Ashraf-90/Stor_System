using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class ProductsDetail
    {
        public ProductsDetail()
        {
            ProjectEquipments = new HashSet<ProjectEquipment>();
        }

        public int ProDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? InventoryId { get; set; }
        public int? SatutsId { get; set; }
        public int? MovementId { get; set; }
        public string? MovementLocation { get; set; }
        public string? ProDetailBarcode { get; set; }
        public string? ProDetailDescription { get; set; }
        public string? Active { get; set; }

        public virtual Inventory? Inventory { get; set; }
        public virtual ProductsModel? Product { get; set; }
        public virtual Satut? Satuts { get; set; }
        public virtual ICollection<ProjectEquipment> ProjectEquipments { get; set; }
    }
}
