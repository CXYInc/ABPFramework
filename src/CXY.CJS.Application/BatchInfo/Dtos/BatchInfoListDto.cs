using System;
using Abp.AutoMapper;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapTo(typeof(Model.BatchInfo))]
    public class BatchInfoListDto
    {
        public string Id { get; set; }
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
        public bool IsDeleted { get; set; }
    }
}