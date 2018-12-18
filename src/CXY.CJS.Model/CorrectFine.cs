using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CorrectFine
    {
        public string WebSiteId { get; set; }
        public string WfdmReviceId { get; set; }
        public int? DdId { get; set; }
        public string Ddname { get; set; }
        public string Ddsort { get; set; }
        public int? Ddlevel { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Wfxw { get; set; }
        public decimal? Fkje { get; set; }
        public string WorkId { get; set; }
        public string PriceSource { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
        public string FromUserId { get; set; }
        public string OldWfdmReviceId { get; set; }
    }
}
