using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblSectors
    {
        public TblSectors()
        {
            TblHouseAddress = new HashSet<TblHouseAddress>();
        }

        public int Id { get; set; }
        public string SectorNo { get; set; }

        public virtual ICollection<TblHouseAddress> TblHouseAddress { get; set; }
    }
}
