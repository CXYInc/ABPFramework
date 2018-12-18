using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CarLocked
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string CarNumber { get; set; }
        public string CarCode { get; set; }
        public string Enginne { get; set; }
        public int Source { get; set; }
        public DateTime LockExpirationDate { get; set; }
        public DateTime CreateTime { get; set; }
        public string CarType { get; set; }
        public bool? PrivateFlag { get; set; }
        public DateTime? RegisterTime { get; set; }
        public int? SeatCount { get; set; }
        public bool? LockFlag { get; set; }
        public string CarTypeName { get; set; }
    }
}
