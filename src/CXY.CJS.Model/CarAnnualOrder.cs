using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CarAnnualOrder
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string CarNumber { get; set; }
        public string CarCode { get; set; }
        public string EngineNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? OrderProxyAmount { get; set; }
        public decimal? Principal { get; set; }
        public decimal? ProxyCharge { get; set; }
        public DateTime Created { get; set; }
        public string Handle { get; set; }
        public string Proxy { get; set; }
        public string ProxyName { get; set; }
        public string OrderType { get; set; }
        public string ProxyAddress { get; set; }
        public string ProxyMobile { get; set; }
        public string ProxyRealName { get; set; }
        public string ProxyZipcode { get; set; }
        public string UserAddress { get; set; }
        public string UserMobile { get; set; }
        public string UserRealName { get; set; }
        public string UserZipcode { get; set; }
        public string Memo { get; set; }
        public DateTime? CarRegisterDate { get; set; }
        public string OrderSource { get; set; }
        public DateTime? PassTime { get; set; }
        public string PassUserId { get; set; }
        public string PassMemo { get; set; }
        public int PassState { get; set; }
        public int State { get; set; }
        public string ExternalPayId { get; set; }
        public DateTime? PayTime { get; set; }
        public int? PayType { get; set; }
        public string Type { get; set; }
        public string Apply { get; set; }
        public DateTime? ApplyTime { get; set; }
        public string CarType { get; set; }
        public string CarSeatType { get; set; }
        public string PrivateFlag { get; set; }
        public string ProxyRemark { get; set; }
        public string IdNumber { get; set; }
        public string UserDoorAddress { get; set; }
        public string UserDoorMobile { get; set; }
        public string UserDoorRealName { get; set; }
        public string Operator { get; set; }
        public string Auduitor { get; set; }
        public int? IsViolation { get; set; }
        public string Dispatched { get; set; }
        public decimal? Vat { get; set; }
        public decimal? TaxRate { get; set; }
        public int? IsInvoice { get; set; }
    }
}
