using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblEmployee
    {
        public TblEmployee()
        {
            TblReceipt = new HashSet<TblReceipt>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public int? RoleId { get; set; }

        public virtual TblEmployeeRole Role { get; set; }
        public virtual ICollection<TblReceipt> TblReceipt { get; set; }
    }
}
