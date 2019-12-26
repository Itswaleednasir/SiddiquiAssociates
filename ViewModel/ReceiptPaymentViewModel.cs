using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyClientCoreProject.ViewModel
{
    public class ReceiptPaymentViewModel
    {
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

        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentDate { get; set; }
        public decimal Balance { get; set; }
        public long? ReceiptId { get; set; }
    }
}
