using System;
using System.Collections.Generic;

namespace Stor_System.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Employees = new HashSet<Employee>();
        }

        public int NationalityId { get; set; }
        public string NationalityName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
