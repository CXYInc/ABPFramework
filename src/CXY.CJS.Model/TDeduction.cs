using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class TDeduction
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string OrderId { get; set; }
        public decimal? NeedDeductAmount { get; set; }
        public int? DeductPart { get; set; }
        public decimal? ReturnAmount { get; set; }
        public int? ReturnPart { get; set; }
        public decimal? SurplusAmount { get; set; }
        public string Operator { get; set; }
        public DateTime? DeductTime { get; set; }
        public string DeductUserId { get; set; }
        public string ReturnUserId { get; set; }
        public string Remark { get; set; }
        public int? DeductType { get; set; }
    }
}
