using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class UserPayRecord
    {
        public string WebSiteId { get; set; }
        public string Payid { get; set; }
        public string Paytype { get; set; }
        public string Paytime { get; set; }
        public DateTime? Created { get; set; }
        public string Sigmsggval { get; set; }
        public decimal? Paytotal { get; set; }
        public string Fromuserid { get; set; }
        public string Memo { get; set; }
        public int? State { get; set; }
        public int WithdrawalsState { get; set; }
        public string Jetxid { get; set; }
        public string CheckResult { get; set; }
        public string ExternalPayId { get; set; }
        public string Operator { get; set; }
    }
}
