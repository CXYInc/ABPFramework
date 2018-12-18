using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class PeccancySearchRecords
    {
        public string Id { get; set; }
        public string WebSite { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string CarNumber { get; set; }
        public string CarCode { get; set; }
        public string EngineNumber { get; set; }
        public string CarType { get; set; }
        public bool PrivateFlag { get; set; }
        public DateTime OccurrenceTime { get; set; }
        public string Location { get; set; }
        public string Reason { get; set; }
        public decimal Count { get; set; }
        public int Degree { get; set; }
        public string Code { get; set; }
        public string Archive { get; set; }
        public decimal Latefine { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string UniqueCode { get; set; }
        public string CarTypeName { get; set; }
    }
}
