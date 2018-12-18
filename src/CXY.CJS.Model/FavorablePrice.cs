using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class FavorablePrice
    {
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CityId { get; set; }
        public int CarNature { get; set; }
        public int CarType { get; set; }
        public int State { get; set; }
        public string PassId { get; set; }
        public string PassName { get; set; }
        public DateTime? PassTime { get; set; }
        public string PassRemark { get; set; }
        public string NoCityId { get; set; }
        public string NoCityName { get; set; }
        public bool IsLock { get; set; }
        public int Kfcount { get; set; }
        public int Bkfcount { get; set; }
        public decimal Bkfprice { get; set; }
        public decimal BkfbeyondPrice { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
