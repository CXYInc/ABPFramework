using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BNoticeMail
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string MailAddress { get; set; }
        public string NoticeTitle { get; set; }
        public string NoticeContent { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
