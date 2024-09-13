using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Role
    {
        public Role()
        {
            UserAccounts = new HashSet<UserAccount>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string Products { get; set; } = null!;
        public string Requests { get; set; } = null!;
        public string Employees { get; set; } = null!;
        public string Categories { get; set; } = null!;
        public string Barnds { get; set; } = null!;
        public string Inventory { get; set; } = null!;
        public string Statuses { get; set; } = null!;
        public string Departments { get; set; } = null!;
        public string Nationalities { get; set; } = null!;
        public string Users_Accounts { get; set; } = null!;
        public string Roles { get; set; } = null!;

        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}
