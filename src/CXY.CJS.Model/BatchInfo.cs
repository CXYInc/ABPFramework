using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class BatchInfo : Entity<string>, IHasModificationTime, IHasDeletionTime, ISoftDelete, IFullAudited
    {
        public BatchInfo()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
            Status = 0;
        }
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string CarCount { get; set; }
        /// <summary>
        /// 批次违章数
        /// </summary>
        public string ViolationCount { get; set; }
        /// <summary>
        /// 需要系统报价数
        /// </summary>
        public string NeedPriceCount { get; set; }
        /// <summary>
        /// 已报价数
        /// </summary>
        public string HadPriceCount { get; set; }


        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }
        public string Customer { get; set; }

        /// <summary>
        /// 黄牛Id
        /// </summary>
        public string Proxy { get; set; }
        public string ProxyUserId { get; set; }

        /// <summary>
        /// 批次状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 批次完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get; set; }
        public long? LastModifierUserId { get; set; }
        public long? DeleterUserId { get; set; }
    }
}
