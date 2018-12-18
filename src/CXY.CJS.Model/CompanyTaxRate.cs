using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CompanyTaxRate
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public string Company { get; set; }
        public decimal? Rate { get; set; }
        public int? RateType { get; set; }
    }
}
