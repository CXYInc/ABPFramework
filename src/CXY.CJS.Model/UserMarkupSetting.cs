using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 针对客户报价加价设置
    /// </summary>
    public class UserMarkupSetting : Entity<string>
    {
        public string WebSiteId { get; set; }
        public string Userid { get; set; }
        public decimal? Gdlr { get; set; }
        public string ProfitJson { get; set; }

        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeleterUserId { get; set; }
    }
}