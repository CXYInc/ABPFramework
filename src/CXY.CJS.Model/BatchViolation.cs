using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchViolation : Entity<string>, IHasCreationTime
    {

        public BatchViolation()
        {

            CreationTime = Clock.Now;
            CanProcess = 0;
        }
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public string CarId { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public int? DataStatus { get; set; }

        /// <summary>
        /// 违章办理状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 违章状态
        /// </summary>
        public int? Status { get; set; }


        /// <summary>
        /// 违章时间
        /// </summary>
        public DateTime ViolationTime { get; set; }

        /// <summary>
        /// 违章代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 文书号
        /// </summary>
        public string Archive { get; set; }

        /// <summary>
        /// 违章地ID
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// 违章城市名称
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 违章地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 违章原因
        /// </summary>
        public string Reason { get; set; }


        /// <summary>
        /// 扣分
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 罚金
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// 滞纳金
        /// </summary>
        public decimal Latefine { get; set; }

        /// <summary>
        /// 增值税
        /// </summary>
        public decimal? Vat { get; set; }
        public decimal? Poundage { get; set; }

        /// <summary>
        /// 是否需要报价
        /// </summary>
        public bool IsAskPrice { get; set; }

        /// <summary>
        /// 违章标识码
        /// </summary>
        public string Uniquecode { get; set; }

        /// <summary>
        /// 现场单/已处理未缴费描述
        /// </summary>
        public string Category { get; set; }       

        /// <summary>
        /// 报价Id
        /// </summary>
        public string QutoPriceId { get; set; }

        /// <summary>
        /// 报价来源
        /// </summary>
        public int? PriceFrom { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int? OrderByNo { get; set; }

        public string ProxyUserId { get; set; }

        public string Proxy { get; set; }

        /// <summary>
        /// 成本价-黄牛报价
        /// </summary>
        public decimal? ProxyPrice { get; set; }


        /// <summary>
        /// 能否办理
        /// </summary>
        public int? CanProcess { get; set; }
        /// <summary>
        /// 办理描述
        /// </summary>
        public string CanprocessMsg { get; set; }

        /// <summary>
        /// 违法类型
        /// </summary>
        public int? ViolationType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 下游备注
        /// </summary>
        public string ProxyRemarks { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
