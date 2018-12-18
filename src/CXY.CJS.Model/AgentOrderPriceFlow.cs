using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class AgentOrderPriceFlow
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public string Applicant { get; set; }
        public string Auditor { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? AuditedDate { get; set; }
        public string ReMarks { get; set; }
        public int? FlowType { get; set; }
        public int? PriceType { get; set; }
    }
}
