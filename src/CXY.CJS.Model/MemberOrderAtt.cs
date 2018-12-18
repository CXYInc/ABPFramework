using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class MemberOrderAtt
    {
        public string Id { get; set; }
        public string MemberOrderId { get; set; }
        public string ValitionId { get; set; }
        public decimal PayAmount { get; set; }
        public DateTime CreateTime { get; set; }
        public string OrderId { get; set; }
        public DateTime? MakeUpPriceTime { get; set; }
        public DateTime? MakeUpPrice { get; set; }
        public int State { get; set; }
        public int NeedFillMoney { get; set; }
        public decimal NeedFillMoneyAmount { get; set; }
        public string NeedFillMoneyReason { get; set; }
        public int NeedFillMaterial { get; set; }
        public string NeedFillMaterialData { get; set; }
        public bool IsSelfCertificate { get; set; }
    }
}
