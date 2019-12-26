using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblEmployeeRole
    {
        public TblEmployeeRole()
        {
            TblEmployee = new HashSet<TblEmployee>();
        }

        public int Id { get; set; }
        public string Role { get; set; }

        public virtual ICollection<TblEmployee> TblEmployee { get; set; }
    }
}
