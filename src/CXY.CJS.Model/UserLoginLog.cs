using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class UserLoginLog
    {
        public string Id { get; set; }
        public DateTime LoginTime { get; set; }
        public string UserId { get; set; }
        public string Ip { get; set; }
        public string WebSiteId { get; set; }
        public string Channel { get; set; }
    }
}
