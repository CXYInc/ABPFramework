using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class OrderDivision
    {
        public string WebSiteId { get; set; }
        public string Fcid { get; set; }
        public string Orderid { get; set; }
        public string Fcuserid { get; set; }
        public decimal? Fc { get; set; }
        public decimal? Gdlr { get; set; }
        public string Fctype { get; set; }
        public string CalculationExpression { get; set; }
        public int? ProfitType { get; set; }
    }
}
