using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class QuotationRuleDetail
    {
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public string ExplainId { get; set; }
        public string Ddbjid { get; set; }
        public int? Ddid { get; set; }
        public string Ddname { get; set; }
        public string LocationId { get; set; }
        public DateTime? Created { get; set; }
        public string Lefthphm { get; set; }
        public string Leftwsbh { get; set; }
        public string Hpzl { get; set; }
        public DateTime? Wftimestart { get; set; }
        public DateTime? Wftimeend { get; set; }
        public decimal? Wfjestart { get; set; }
        public decimal? Wfjeend { get; set; }
        public string Blsm { get; set; }
        public int? State { get; set; }
        public string Hpzlmc { get; set; }
        public string PriceSource { get; set; }
        public string CarNature { get; set; }
        public string BlurLocation { get; set; }
        public string Location { get; set; }
        public string NeedMakeDataEnum { get; set; }
        public int? Ischoose { get; set; }
        public string PriceBasic { get; set; }
        public int Price { get; set; }
        public int? PlusPrice { get; set; }
    }
}
