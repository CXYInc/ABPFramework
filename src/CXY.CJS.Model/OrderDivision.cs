using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public class OrderDivision : Entity<string>
    {
        public string WebSiteId { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Gdlr { get; set; }
        public string CalculationExpression { get; set; }
        public int ProfitType { get; set; }
        public string ProfitTypeName { get; set; }
    }
}
