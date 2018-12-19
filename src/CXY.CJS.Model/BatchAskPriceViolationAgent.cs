using System;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CXY.CJS.Model
{
    public  class BatchAskPriceViolationAgent:Entity<string>,IHasCreationTime
    {
        public string WebSiteId { get; set; }
        public string CarId { get; set; }
        public int State { get; set; }
        public DateTime ViolationTime { get; set; }
        public string Archive { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }
        public int Degree { get; set; }
        public decimal Count { get; set; }
        public decimal Latefine { get; set; }
        public decimal? Poundage { get; set; }
        public decimal? FirstPoundage { get; set; }
        public bool IsAskPrice { get; set; }
        public string Uniquecode { get; set; }
        public decimal? LastTimePoundage { get; set; }
        public int? Status { get; set; }
        public string FavorablePriceInfo { get; set; }
        public string OrderJsonSelectId { get; set; }
        public int? CanProcess { get; set; }
        public string Category { get; set; }
        public decimal? Vat { get; set; }
        public decimal? LockPoundage { get; set; }
        public decimal? CommonPoundage { get; set; }
        public string Ddbjid { get; set; }
        public int? PriceFrom { get; set; }
        public int? OrderByNo { get; set; }
        public string AgentUserId { get; set; }
        public decimal? AgentPrice { get; set; }
        public string AgentUserName { get; set; }
        public string CanprocessMsg { get; set; }
        public int? DataStatus { get; set; }
        public int? ViolationType { get; set; }
        public string Remarks { get; set; }
        public string ProxyRemarks { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
