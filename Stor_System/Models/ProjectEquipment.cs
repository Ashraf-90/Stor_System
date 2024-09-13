using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class ProjectEquipment
    {
        public int PeId { get; set; }
        public int ProjectId { get; set; }
        public int? ProDetailId { get; set; }

        public virtual ProductsDetail? ProDetail { get; set; }
        public virtual Project Project { get; set; } = null!;
    }
}
