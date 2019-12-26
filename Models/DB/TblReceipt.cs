using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblReceipt
    {
        public TblReceipt()
        {
            TblPayments = new HashSet<TblPayments>();
        }

        public long Id { get; set; }
        public string Sno { get; set; }
        public int? FileId { get; set; }
        public int? HouseId { get; set; }
        public int? MessrsId { get; set; }
        public string Description { get; set; }
        public decimal? CaseTotalCost { get; set; }
        public string ReceiptDate { get; set; }
        public int? RegisterMessrsId { get; set; }
        public string RegisterDate { get; set; }
        public decimal? RegisterAmount { get; set; }
        public string DiaryNo { get; set; }
        public string DiaryDate { get; set; }
        public string DispatchNo { get; set; }
        public string DispatchDate { get; set; }
        public decimal? StampDutyAmount { get; set; }
        public string StampDutyDate { get; set; }
        public string ChallanNo { get; set; }
        public string ChallanDate { get; set; }
        public int? EmployeeId { get; set; }

        public virtual TblEmployee Employee { get; set; }
        public virtual TblFile File { get; set; }
        public virtual TblHouseAddress House { get; set; }
        public virtual TblMessrs Messrs { get; set; }
        public virtual TblRegisterMessrs RegisterMessrs { get; set; }
        public virtual ICollection<TblPayments> TblPayments { get; set; }
    }
}
