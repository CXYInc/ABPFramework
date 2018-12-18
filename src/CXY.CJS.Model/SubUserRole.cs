using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class SubUserRole
    {
        public string Roleid { get; set; }
        public string WebSiteId { get; set; }
        public string Userid { get; set; }
        public string Rolename { get; set; }
        public int? Isdelete { get; set; }
        public DateTime? Uptime { get; set; }
        public string Menu { get; set; }
        public string Remark { get; set; }
    }
}
