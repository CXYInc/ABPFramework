using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class ThirdPartyUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Status { get; set; }
        public DateTime? RgistDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Remark { get; set; }
        public string CallbackUrl { get; set; }
        public string ProxyIds { get; set; }
    }
}
