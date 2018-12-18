using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class ViolationCode
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public string Code { get; set; }
        public string Fine { get; set; }
        public string Reason { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Source { get; set; }
    }
}
