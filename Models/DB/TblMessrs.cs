using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblMessrs
    {
        public TblMessrs()
        {
            TblReceipt = new HashSet<TblReceipt>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }

        public virtual ICollection<TblReceipt> TblReceipt { get; set; }
    }
}
