using System;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    /// <summary>
    /// WebSite 扩展表
    /// </summary>
    public class WebSiteExtend : Entity<string>
    {

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


    }
}