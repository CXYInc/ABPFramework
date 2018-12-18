using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class OrderDetail
    {
        public string WebSiteId { get; set; }
        public string Orderid { get; set; }
        public int? State { get; set; }
        public string Apply { get; set; }
        public string Applytime { get; set; }
        public string Passid { get; set; }
        public DateTime? Created2 { get; set; }
        public int? Isdestory { get; set; }
        public int? Isbackfee { get; set; }
        public int? Iscallback { get; set; }
        public int IorderState { get; set; }
        public DateTime? IorderTime { get; set; }
        public int? InterfaceType { get; set; }
        public string IorderId { get; set; }
        public DateTime? IlastTime { get; set; }
        public decimal OrderCost { get; set; }
        public string ExternalPayId { get; set; }
        public string SurePeople { get; set; }
        public string PaymentNumber { get; set; }
        public string PassMemo { get; set; }
        public bool OrderCallBackFlag { get; set; }
        public int OrderCallBackCount { get; set; }
        public DateTime? OrderCallBackDate { get; set; }
        public DateTime? FillMoneyCallBackDate { get; set; }
        public int FillMoneyCallBackCount { get; set; }
        public bool FillMoneyCallBackFlag { get; set; }
        public DateTime? FillMaterialCallBackDate { get; set; }
        public int FillMaterialCallBackCount { get; set; }
        public bool FillMaterialCallBackFlag { get; set; }
        public string Operator { get; set; }
        public string DistributeOperator { get; set; }
        public string ProxyOperator { get; set; }
        public string Approver { get; set; }
        public string NeedMakeUpPriceOperator { get; set; }
        public string NeedMakeUpDataOperator { get; set; }
        public string NeedMakeUpPriceApprover { get; set; }
        public string MakeUpPriceOperator { get; set; }
        public string MakeUpDataOperator { get; set; }
        public string BackOperator { get; set; }
        public string MakeUpPriceApprover { get; set; }
        public DateTime? OrderPayStatusCallBackDate { get; set; }
        public int OrderPayStatusCallBackCount { get; set; }
        public bool OrderPayStatusCallBackFlag { get; set; }
        public int? IsApplyBack { get; set; }
        public DateTime? CancelApplyTime { get; set; }
        public int? CancelApplyMemo { get; set; }
        public string CancelApplyPassId { get; set; }
        public int? IsNewOrder { get; set; }
        public int? IsNewDynamic { get; set; }
        public int? ExportNum { get; set; }
    }
}
