using System;
using System.Collections.Generic;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    public  class CarViolationDivision:Entity<string>
    {
        public string WebSiteId { get; set; }
        //public string Fcid { get; set; }
        public string ViolationId { get; set; }
        public string Fcuserid { get; set; }
        public decimal? Fc { get; set; }
        public decimal? Gdlr { get; set; }
        public string Fctype { get; set; }
        public string CalculationExpression { get; set; }
        public int? ProfitType { get; set; }
    }
}
