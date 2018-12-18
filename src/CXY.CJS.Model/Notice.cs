using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 站内信
    /// </summary>
    public class Notice : Entity<string>, IHasCreationTime
    {
        public int NoticeType { get; set; }
        public string NoticeTitle { get; set; }
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public string FromUserId { get; set; }
        public string NoticeContent { get; set; }
        public int Staus { get; set; }
        public DateTime ValidityDate { get; set; }
        public int? IsRead { get; set; }
        public string FromUserName { get; set; }
        public string Operator { get; set; }
        public DateTime CreationTime { get; set; }
    }
}