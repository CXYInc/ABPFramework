using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class SysRole
    {
        public int Roleid { get; set; }
        public string Rolename { get; set; }
        public string Rolems { get; set; }
        public int? Weight { get; set; }
        public int? Isdelete { get; set; }
        public DateTime? Uptime { get; set; }
    }
}
