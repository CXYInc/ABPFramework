using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CarInfoCi
    {
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public string IncBatchId { get; set; }
        public string CarNumber { get; set; }
        public string CarCode { get; set; }
        public string EngineNo { get; set; }
        public int? PrivateCar { get; set; }
        public string CarType { get; set; }
        public string CarTypeName { get; set; }
        public int? IsLock { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string DriverLicense { get; set; }
        public bool? IsNeedSearch { get; set; }
        public string VolotionMsg { get; set; }
        public bool HaveLockRule { get; set; }
        public DateTime CreateTime { get; set; }
        public string CompanyName { get; set; }
    }
}
