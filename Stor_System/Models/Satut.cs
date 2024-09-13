using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Satut
    {
        public Satut()
        {
            ProductsDetails = new HashSet<ProductsDetail>();
        }

        public int SatutsId { get; set; }
        public string? SatutsName { get; set; }
        public string? Color { get; set; }
        public string? Usability { get; set; }

        public virtual ICollection<ProductsDetail> ProductsDetails { get; set; }
    }
}
