using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.ViewModel
{
    public class GetReceiptViewModel
    {
        public string ReceiptDate { get; set; }
        public string SNo { get; set; }
        public string FileNo { get; set; }
        public string SectorNo { get; set; }
        public string HouseNo { get; set; }
        public string MessrName { get; set; }
        public string Description { get; set; }
        public decimal? TotalAmount { get; set; }
        public string RegisterDate { get; set; }
        public string RegisterMessrName { get; set; }
        public decimal? RegisterAmount { get; set; }
        public string DiaryDate { get; set; }
        public string DiaryNo { get; set; }
        public string DispatchDate { get; set; }
        public string DispatchNo { get; set; }
        public string StampDutyDate { get; set; }
        public decimal? StampDutyAmount { get; set; }
        public string ChallanDate { get; set; }
        public decimal? ChallanAmount { get; set; }
        public decimal? FileExpenditure { get; set; }
        public decimal? TotalExpenditure { get; set; }
        public string PaymentDate { get; set; }
        public decimal Payment { get; set; }
        public decimal Balance { get; set; }
        
    }
}
