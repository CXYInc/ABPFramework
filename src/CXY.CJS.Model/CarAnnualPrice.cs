using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CarAnnualPrice
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public decimal? UnOnlinePrincipal { get; set; }
        public decimal? UnOnlineCharge { get; set; }
        public decimal? CarPrincipal { get; set; }
        public decimal? CarCharge { get; set; }
        public decimal? MotoPrincipal { get; set; }
        public decimal? MotoCharge { get; set; }
        public decimal? A1principal { get; set; }
        public decimal? A1charge { get; set; }
        public decimal? A2principal { get; set; }
        public decimal? A2charge { get; set; }
        public decimal? B1principal { get; set; }
        public decimal? B1charge { get; set; }
        public decimal? B2principal { get; set; }
        public decimal? B2charge { get; set; }
        public string Memo { get; set; }
        public int FromAddressId { get; set; }
        public string FromAddressName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? PassTime { get; set; }
        public string PassUserId { get; set; }
        public string PassMemo { get; set; }
        public int PassState { get; set; }
        public string Type { get; set; }
        public string FromId { get; set; }
        public string Operator { get; set; }
        public string Approver { get; set; }
        public int? IsDoor { get; set; }
    }
}
