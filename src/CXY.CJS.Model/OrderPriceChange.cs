using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class OrderPriceChange
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int? ChangeType { get; set; }
        public int? Status { get; set; }
        public decimal? Fine { get; set; }
        public decimal? LateFee { get; set; }
        public decimal? ServiceCharge { get; set; }
        public string Creator { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Auditor { get; set; }
        public string AuditorUserId { get; set; }
        public DateTime? AuditionTime { get; set; }
        public string Remark { get; set; }
    }
}
