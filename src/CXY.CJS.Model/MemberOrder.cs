using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class MemberOrder
    {
        public string Id { get; set; }
        public string MemeberId { get; set; }
        public decimal PayAmount { get; set; }
        public decimal CopeWithAmount { get; set; }
        public string PayType { get; set; }
        public int State { get; set; }
        public string BatchId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Phone { get; set; }
        public string MchId { get; set; }
        public bool SyncPayStatus { get; set; }
        public string Extend { get; set; }
    }
}
