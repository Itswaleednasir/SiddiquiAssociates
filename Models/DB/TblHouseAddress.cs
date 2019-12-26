using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblHouseAddress
    {
        public TblHouseAddress()
        {
            TblReceipt = new HashSet<TblReceipt>();
        }

        public int Id { get; set; }
        public string HouseNo { get; set; }
        public int? SectorId { get; set; }

        public virtual TblSectors Sector { get; set; }
        public virtual ICollection<TblReceipt> TblReceipt { get; set; }
    }
}
