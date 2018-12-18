using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class AccountApplyInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Memo { get; set; }
        public string WebSiteId { get; set; }
        public int State { get; set; }
        public string SetMemo { get; set; }
        public DateTime? SetTime { get; set; }
    }
}
