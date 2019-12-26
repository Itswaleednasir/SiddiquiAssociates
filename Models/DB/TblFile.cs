using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblFile
    {
        public TblFile()
        {
            TblReceipt = new HashSet<TblReceipt>();
        }

        public int Id { get; set; }
        public string FileNo { get; set; }

        public virtual ICollection<TblReceipt> TblReceipt { get; set; }
    }
}
