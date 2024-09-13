using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class UserAccount
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Passwod { get; set; } = null!;
        public string Active { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
