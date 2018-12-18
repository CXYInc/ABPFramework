using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class OrderPriceChangeCi
    {
        public string PriceId { get; set; }
        public string ViolationId { get; set; }
        public string CarNumber { get; set; }
        public decimal? MakePrice { get; set; }
        public int? MakeTimes { get; set; }
        public int? MakeStatus { get; set; }
        public DateTime Created { get; set; }
        public string PassMemo { get; set; }
        public string Memo { get; set; }
    }
}
