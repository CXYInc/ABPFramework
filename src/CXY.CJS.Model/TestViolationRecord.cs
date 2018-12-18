using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class TestViolationRecord
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CarNumber { get; set; }
        public string CarType { get; set; }
        public bool PrivateFlag { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public DateTime? Time { get; set; }
        public decimal Count { get; set; }
        public int Degree { get; set; }
        public string Code { get; set; }
        public string Archive { get; set; }
        public string Category { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public bool? IsLock { get; set; }
    }
}
