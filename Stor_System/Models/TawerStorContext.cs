using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stor_System.Models
{
    public partial class TawerStorContext : DbContext
    {
        public TawerStorContext()
        {
        }

        public TawerStorContext(DbContextOptions<TawerStorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Nationality> Nationalities { get; set; } = null!;
        public virtual DbSet<ProductsDetail> ProductsDetails { get; set; } = null!;
        public virtual DbSet<ProductsModel> ProductsModels { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectEquipment> ProjectEquipments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SLocation> SLocations { get; set; } = null!;
        public virtual DbSet<Satut> Satuts { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ED99E7K\\SQLEXPRESS; Database=TawerStor; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.BrandName).HasColumnName("Brand_Name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.CategoryName).HasColumnName("Category_Name");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_Id");

                entity.Property(e => e.DepartmentName).HasColumnName("Department_Name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_Id");

                entity.Property(e => e.EmployeeName).HasColumnName("Employee_Name");

                entity.Property(e => e.ImagePath).HasColumnName("Image_Path");

                entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");

                entity.Property(e => e.Qid).HasColumnName("QID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.NationalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Nationality");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.InventoryId).HasColumnName("Inventory_Id");

                entity.Property(e => e.GMapLink).HasColumnName("G_Map_Link");

                entity.Property(e => e.InventoryLocation).HasColumnName("Inventory_Location");

                entity.Property(e => e.InventoryName).HasColumnName("Inventory_Name");

                entity.Property(e => e.InventoryPhone).HasColumnName("Inventory_Phone");

                entity.Property(e => e.SLocationId).HasColumnName("S_location_Id");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.ToTable("Nationality");

                entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");

                entity.Property(e => e.NationalityName)
                    .HasMaxLength(200)
                    .HasColumnName("Nationality_Name");
            });

            modelBuilder.Entity<ProductsDetail>(entity =>
            {
                entity.HasKey(e => e.ProDetailId);

                entity.ToTable("Products_Details");

                entity.Property(e => e.ProDetailId).HasColumnName("Pro_Detail_Id");

                entity.Property(e => e.InventoryId).HasColumnName("Inventory_Id");

                entity.Property(e => e.MovementId).HasColumnName("Movement_Id");

                entity.Property(e => e.MovementLocation).HasColumnName("Movement_Location");

                entity.Property(e => e.ProDetailBarcode).HasColumnName("Pro_Detail_Barcode");

                entity.Property(e => e.ProDetailDescription).HasColumnName("Pro_Detail_Description");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.SatutsId).HasColumnName("Satuts_Id");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.ProductsDetails)
                    .HasForeignKey(d => d.InventoryId)
                    .HasConstraintName("FK_Products_Details_Inventories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Products_Details_Products_Models");

                entity.HasOne(d => d.Satuts)
                    .WithMany(p => p.ProductsDetails)
                    .HasForeignKey(d => d.SatutsId)
                    .HasConstraintName("FK_Products_Details_Satuts");
            });

            modelBuilder.Entity<ProductsModel>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("Products_Models");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.ImagePath).HasColumnName("Image_Path");

                entity.Property(e => e.ProductModel).HasColumnName("Product_Model");

                entity.Property(e => e.ProductName).HasColumnName("Product_Name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.ProductsModels)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Models_Brands");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductsModels)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Models_Categories");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.Property(e => e.ApproveStatus).HasColumnName("Approve_Status");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("Create_Date");

                entity.Property(e => e.EmployeeDeliverId).HasColumnName("Employee_Deliver_Id");

                entity.Property(e => e.EmployeeRequesterId).HasColumnName("Employee_Requester_Id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("End_Date");

                entity.Property(e => e.NotifivationSeen).HasColumnName("Notifivation_Seen");

                entity.Property(e => e.ProjectDescription).HasColumnName("Project_Description");

                entity.Property(e => e.ProjectLocation).HasColumnName("Project_Location");

                entity.Property(e => e.ProjectName).HasColumnName("Project_Name");

                entity.Property(e => e.ProjectStatus).HasColumnName("Project_Status");

                entity.Property(e => e.InternalExternal).HasColumnName("Internal_External");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("Start_Date");

                entity.HasOne(d => d.EmployeeDeliver)
                    .WithMany(p => p.ProjectEmployeeDelivers)
                    .HasForeignKey(d => d.EmployeeDeliverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Employee1");

                entity.HasOne(d => d.EmployeeRequester)
                    .WithMany(p => p.ProjectEmployeeRequesters)
                    .HasForeignKey(d => d.EmployeeRequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Employee");
            });

            modelBuilder.Entity<ProjectEquipment>(entity =>
            {
                entity.HasKey(e => e.PeId);

                entity.ToTable("Project_Equipment");

                entity.Property(e => e.PeId).HasColumnName("PE_ID");

                entity.Property(e => e.ProDetailId).HasColumnName("Pro_Detail_Id");

                entity.Property(e => e.ProjectId).HasColumnName("Project_Id");

                entity.HasOne(d => d.ProDetail)
                    .WithMany(p => p.ProjectEquipments)
                    .HasForeignKey(d => d.ProDetailId)
                    .HasConstraintName("FK_Project_Equipment_Products_Details");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectEquipments)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Equipment_Project");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.Property(e => e.RoleId).HasColumnName("Role_Id");
                entity.Property(e => e.RoleName).HasColumnName("Role_name");
                entity.Property(e => e.Products).HasColumnName("Products");
                entity.Property(e => e.Requests).HasColumnName("Requests");
                entity.Property(e => e.Employees).HasColumnName("Employees");
                entity.Property(e => e.Categories).HasColumnName("Categories");
                entity.Property(e => e.Barnds).HasColumnName("Barnds");
                entity.Property(e => e.Inventory).HasColumnName("Inventory");
                entity.Property(e => e.Statuses).HasColumnName("Statuses");
                entity.Property(e => e.Departments).HasColumnName("Departments");
                entity.Property(e => e.Nationalities).HasColumnName("Nationalities");
                entity.Property(e => e.Users_Accounts).HasColumnName("Users_Accounts");
                entity.Property(e => e.Roles).HasColumnName("Roles");
            });

            modelBuilder.Entity<SLocation>(entity =>
            {
                entity.ToTable("S_locations");

                entity.Property(e => e.SLocationId).HasColumnName("S_location_Id");

                entity.Property(e => e.SLocationName).HasColumnName("S_location_Name");
            });

            modelBuilder.Entity<Satut>(entity =>
            {
                entity.HasKey(e => e.SatutsId);

                entity.Property(e => e.SatutsId).HasColumnName("Satuts_Id");

                entity.Property(e => e.SatutsName).HasColumnName("Satuts_Name");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_Account");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UserName).HasColumnName("User_Name");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Account_Employee");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Account_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
