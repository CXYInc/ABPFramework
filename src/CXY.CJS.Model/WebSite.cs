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

        public DateTime EndTime { get; set; }

        public string ConnectionString { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }


        public string WebSiteId { get; set; }
        /// <summary>
        /// 域名集合,号分割多个域名
        /// </summary>	
        public string WebSiteDomains { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>		
        public string WorkerName { get; set; }

        /// <summary>
        /// 服务qq
        /// </summary>
        public string CustQq { get; set; }

        /// <summary>
        /// 服务电话
        /// </summary>
        public string CustSerPhone { get; set; }

        /// <summary>
        /// 站点备注
        /// </summary>		
        public string WebSiteMemo { get; set; }

        /// <summary>
        /// 默认车牌简称
        /// </summary>
        public string DefaultCarNumberForShort { get; set; }


        public string ConcernArea { get; set; }


        public string Copyright { get; set; }

        public string SiteLoginImage { get; set; }

        public string H5ImgUrl { get; set; }

        public DateTime H5ImgAddTime { get; set; }

        /// <summary>
        /// 是否进行罚款修正
        /// </summary>
        public int IsRevice { get; set; }

        public int IsDownApp { get; set; }

        public int IsInvoice { get; set; }

        public decimal TaxRate { get; set; }


        public WebSite()
        {
            CreationTime = Clock.Now;
        }

    }
}
