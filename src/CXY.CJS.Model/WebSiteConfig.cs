using System;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 站点配置信息
    /// </summary>
    public class WebSiteConfig : Entity<string>
    {
        /// <summary>
        /// 站点备注
        /// </summary>		
        public string WebSiteMemo { get; set; }

        /// <summary>
        /// 订单快速办理费用
        /// </summary>
        public decimal QuickAmount { get; set; }

        /// <summary>
        /// 分成表达式
        /// </summary>
        public int VisibleCalculationExpression { get; set; }

        /// <summary>
        /// 网站扣分单利润？
        /// </summary>
        public decimal WebFixedProfit { get; set; }

        public int MasterAgentDefaultGivePoints { get; set; }


        public int FirstAgentDefaultGivePoints { get; set; }

        public int SecondAgentDefaultGivePoints { get; set; }

        public int GivePointsPerMonth { get; set; }

        public decimal DefaultJfPrice { get; set; }

        public decimal DefaultNotePrice { get; set; }

        public decimal ReceivableAmount { get; set; }

        public DateTime ReceivableDate { get; set; }

        public string ExpirationReminder { get; set; }

        public int GivePointsSurplusSameMonth { get; set; }

      

        public int SmsSendInterval { get; set; }

        public string AskPriceMailAddress { get; set; }
    }
}