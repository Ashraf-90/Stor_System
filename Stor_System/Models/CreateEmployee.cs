using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class CreateEmployee
    {
        public CreateEmployee()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int EmployeeId { get; set; }
        public int NationalityId { get; set; }
        public int DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string BirthDay { get; set; }
        public string? Qid { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public IFormFile? ImagePath { get; set; }
        public string? Active { get; set; }

        public virtual Department Department { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
