using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ProjectEmployeeDelivers = new HashSet<Project>();
            ProjectEmployeeRequesters = new HashSet<Project>();
            UserAccounts = new HashSet<UserAccount>();
        }

        public int EmployeeId { get; set; }
        public int NationalityId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string? BirthDay { get; set; }
        public string? Qid { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImagePath { get; set; }
        public string? Active { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Nationality Nationality { get; set; } = null!;
        public virtual ICollection<Project> ProjectEmployeeDelivers { get; set; }
        public virtual ICollection<Project> ProjectEmployeeRequesters { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
