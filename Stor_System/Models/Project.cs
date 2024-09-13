using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stor_System.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectEquipments = new HashSet<ProjectEquipment>();
        }

        public int ProjectId { get; set; }
        public int EmployeeRequesterId { get; set; }
        public int EmployeeDeliverId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string? ProjectLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ApproveStatus { get; set; }
        public string? ProjectStatus { get; set; }
        public string? InternalExternal { get; set; }
        public string? NotifivationSeen { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Active { get; set; }

        public virtual Employee EmployeeDeliver { get; set; } = null!;
        public virtual Employee EmployeeRequester { get; set; } = null!;
        public virtual ICollection<ProjectEquipment> ProjectEquipments { get; set; }


        [NotMapped]
        public int Quantity { get; set; }

        [NotMapped]
        public Dictionary<(string ProductName, string ModelName), int> ProductModelCounts { get; set; }
    }
}
