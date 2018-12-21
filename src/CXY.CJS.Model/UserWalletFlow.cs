using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 余额流水表
    /// </summary>
    public class UserWalletFlow : Entity<string>, IHasCreationTime
    {
        public string WebSiteId { get; set; }
        /// <summary>
        /// 流水类型Id
        /// </summary>
        public int FlowType { get; set; }
        /// <summary>
        /// 流水类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 发生金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 发生后金额
        /// </summary>
        public decimal AfterAmount { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public UserWalletFlow()
        {
            CreationTime = Clock.Now;
        }

    }
}
