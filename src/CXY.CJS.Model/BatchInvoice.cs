using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchInvoice
    {
        public string BatchId { get; set; }
        public decimal InvoiceValue { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EditeTime { get; set; }
        public string LastEditedBy { get; set; }
        public string LastEditedByName { get; set; }
    }
}
