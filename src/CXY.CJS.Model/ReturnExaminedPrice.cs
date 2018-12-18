using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class ReturnExaminedPrice
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public string Memo { get; set; }
        public int? FromAddressId { get; set; }
        public string FromAddressName { get; set; }
        public decimal? Principal { get; set; }
        public decimal? Charge { get; set; }
        public string ProxyRealName { get; set; }
        public string ProxyMobile { get; set; }
        public string ProxyAddress { get; set; }
        public DateTime? PriceTime { get; set; }
        public string CarNumber { get; set; }
        public string CarCode { get; set; }
        public string EngineNumber { get; set; }
        public string CarType { get; set; }
        public DateTime? RegisterTime { get; set; }
        public string PrivateFlag { get; set; }
        public string SeatNumber { get; set; }
        public string IdCard { get; set; }
        public string Type { get; set; }
        public string PriceId { get; set; }
        public int? IsDoor { get; set; }
    }
}
