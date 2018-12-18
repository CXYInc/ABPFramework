using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchReceivables
    {
        public string Id { get; set; }
        public string BatchId { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public decimal? Amount { get; set; }
        public string RecvChannel { get; set; }
        public string BankSerialNum { get; set; }
        public string Remark { get; set; }
        public string HandleId { get; set; }
        public string Handler { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
