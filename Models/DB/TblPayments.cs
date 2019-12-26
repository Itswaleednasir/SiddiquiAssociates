using System;
using System.Collections.Generic;

namespace MyClientCoreProject.Models.DB
{
    public partial class TblPayments
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string PaymentDate { get; set; }
        public decimal Balance { get; set; }
        public long? ReceiptId { get; set; }

        public virtual TblReceipt Receipt { get; set; }
    }
}
