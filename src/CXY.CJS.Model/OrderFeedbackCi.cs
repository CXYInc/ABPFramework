using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class OrderFeedbackCi
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public int? OrderEnum { get; set; }
        public string ViolationId { get; set; }
        public string DataUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
