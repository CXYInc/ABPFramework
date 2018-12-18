using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class SysMenu
    {
        public int Siteid { get; set; }
        public int? Parentid { get; set; }
        public string Sitename { get; set; }
        public int? Siteleval { get; set; }
        public string Sitelayer { get; set; }
        public string Sitetarget { get; set; }
        public string Siteurl { get; set; }
        public int? Isout { get; set; }
        public int? Weight { get; set; }
        public int? Isdelete { get; set; }
        public DateTime? Uptime { get; set; }
        public int? Isparent { get; set; }
        public int? Projectid { get; set; }
        public int? Issys { get; set; }
    }
}
