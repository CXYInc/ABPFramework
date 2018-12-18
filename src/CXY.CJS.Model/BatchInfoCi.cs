using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchInfoCi
    {
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public DateTime? BeginTime { get; set; }
        public int CarCount { get; set; }
        public int ViolationCount { get; set; }
        public int PriceViolationCount { get; set; }
        public decimal? PriceSum { get; set; }
        public decimal? BudgetSum { get; set; }
        public int? RobState { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? LastEditedTime { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? DeadLine { get; set; }
        public string Remark { get; set; }
    }
}
