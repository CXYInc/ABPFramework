using Abp.Domain.Entities;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 站点基础信息
    /// </summary>
    public class WebSite : Entity<string>
    {
        public string WebSiteName { get; set; }

        /// <summary>
        /// 订单Id前缀
        /// </summary>
        public string WebSiteKey { get; set; }

        public string WebSiteType { get; set; }

        public string WebSiteMater { get; set; }

        public string EndTime { get; set; }

        public string ConnectionString { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public WebSite()
        {
            CreationTime = Clock.Now;
        }

    }
}
