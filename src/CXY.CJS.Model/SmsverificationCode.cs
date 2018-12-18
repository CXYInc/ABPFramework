using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class SmsverificationCode
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string VerificationCode { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public string Telephone { get; set; }
        public string WebSiteId { get; set; }
    }
}
