using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CashRecord
    {
        public string WebSiteId { get; set; }
        public string Jetxid { get; set; }
        public DateTime? Created { get; set; }
        public string Fromuserid { get; set; }
        public decimal? Txje { get; set; }
        public decimal? Txhje { get; set; }
        public string Memo { get; set; }
        public int? State { get; set; }
        public string Passwordid { get; set; }
        public string Passtime { get; set; }
        public string Txzh { get; set; }
        public string Txkhx { get; set; }
        public string Txskr { get; set; }
        public string Txsj { get; set; }
        public string CheckResult { get; set; }
        public string Txyh { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Operator { get; set; }
        public string Approver { get; set; }
    }
}
