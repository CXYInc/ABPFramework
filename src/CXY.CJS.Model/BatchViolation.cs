﻿using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchViolation
    {
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
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
        public decimal CarPoundage { get; set; }
        public decimal ReviceCount { get; set; }
        public string PlaceOrderJsonSelectId { get; set; }
        public decimal Vat { get; set; }
    }
}
