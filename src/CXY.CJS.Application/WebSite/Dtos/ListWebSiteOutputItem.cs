using System;

namespace CXY.CJS.Application.Dtos
{
    public class ListWebSiteOutputItem
    {
        public string WebSiteId { get; set; }

        public string WebSiteName { get; set; }

        public string WebSiteKey { get; set; }

        public string WebSiteDomains { get; set; }

        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>		
        public string WorkerName { get; set; }

        /// <summary>
        /// 站点备注
        /// </summary>		
        public string WebSiteMemo { get; set; }

        public string WebSiteMater { get; set; }

        /// <summary>
        /// 订单快速办理费用
        /// </summary>
        public decimal QuickAmount { get; set; }

        public int AlipayGiro { get; set; }

        /// <summary>
        /// 服务电话
        /// </summary>
        public string CustSerPhone { get; set; }

        public string CustQQ { get; set; }

        /// <summary>
        /// 默认车牌简称
        /// </summary>
        public string DefaultCarNumberForShort { get; set; }

        /// <summary>
        /// 订单赠送积分
        /// </summary>
        public int OrderGiveNum { get; set; }


        public string WebSiteType { get; set; }


        /// <summary>
        /// 扣分单自动分单？
        /// </summary>
        public int AutoOrderShunt { get; set; }

        /// <summary>
        /// 是否开通余额支付
        /// </summary>
        public int IsBalancePayment { get; set; }

        public int IsWeChatPayment { get; set; }

        public int IsAlipayPayment { get; set; }

        public string AlipaySellerEmail { get; set; }

        public string AlipayKey { get; set; }
        public string AlipayPartner { get; set; }
        public int IsUseSysAlipay { get; set; }
        public string AlipayCodeUrl { get; set; }
        public string ConcernArea { get; set; }
        public DateTime EndTime { get; set; }
        public int VisibleCalculationExpression { get; set; }
        public decimal WebFixedProfit { get; set; }
        public int MasterAgentDefaultGivePoints { get; set; }
        public int FirstAgentDefaultGivePoints { get; set; }
        public int SecondAgentDefaultGivePoints { get; set; }
        public int GivePointsPerMonth { get; set; }
        public string Copyright { get; set; }
        public string WxappID { get; set; }
        public string Wxmchid { get; set; }
        public string Wxkey { get; set; }
        public string WxsubAppid { get; set; }
        public string WxsubMchId { get; set; }
        public string WxsubKey { get; set; }
        public int IsUseSysWeiXinPay { get; set; }
        public string SiteLoginImage { get; set; }
        public int WeiXinGiro { get; set; }
        public string WeiXinCodeUrl { get; set; }
        public string AlipayAppID { get; set; }
        public string AlipayPublicKey { get; set; }
        public string AlipayPrivateKey { get; set; }
        public int IsWFTPayment { get; set; }
        public string WFTMchid { get; set; }
        public string WFTKey { get; set; }
        public string H5ImgUrl { get; set; }
        public string H5ImgAddTime { get; set; }
        public decimal DefaultJFPrice { get; set; }
        public decimal DefaultNotePrice { get; set; }
        public int IsRevice { get; set; }
        public int IsDownApp { get; set; }
        public decimal ReceivableAmount { get; set; }
        public string ReceivableDate { get; set; }
        public int DefaultCheckLock { get; set; }
        public string ExpirationReminder { get; set; }
        public int GivePointsSurplusSameMonth { get; set; }
        public bool PayForAnother { get; set; }
        public int SMSSendInterval { get; set; }
        public string AskPriceMailAddress { get; set; }
        public int IsInvoice { get; set; }
        public decimal TaxRate { get; set; }

    }
}