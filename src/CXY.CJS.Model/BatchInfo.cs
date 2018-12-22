using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class BatchInfo : Entity<string>
    {
        public BatchInfo()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
            Status = 0;
        }
        public string WebSiteId { get; set; }

        /// <summary>
        /// 车辆数
        /// </summary>
        public int CarCount { get; set; }

        /// <summary>
        /// 批次违章数
        /// </summary>
        public int ViolationCount { get; set; }
        /// <summary>
        /// 需要系统报价数
        /// </summary>
        public int NeedPriceCount { get; set; }
        /// <summary>
        /// 已报价数
        /// </summary>
        public int HadPriceCount { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }
        public string Customer { get; set; }

        /// <summary>
        /// 黄牛名字
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        /// 黄牛Id
        /// </summary>
        public string ProxyUserId { get; set; }

        public string Remark { get; set; }

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

        public string CreatorUserId { get; set; }

        public string LastModifierUserId { get; set; }

        public string DeleterUserId { get; set; }
    }
}
