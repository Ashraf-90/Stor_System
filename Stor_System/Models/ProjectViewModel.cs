namespace Stor_System.Models
{
    public class ProjectViewModel
    {
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
        public string? NotifivationSeen { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Active { get; set; }
        public ICollection<ProductsModelViewModel> ProductsModels { get; set; } = new List<ProductsModelViewModel>();
    }

    public class ProductsModelViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int ProductsDetailCount { get; set; }
    }
}
