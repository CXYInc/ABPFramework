using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    /// <summary>
    ///  WebSite 支付配置表
    /// </summary>
    public class WebSitePayConfig : Entity<string>
    {
        public string WebSiteId { get; set; }

        public bool PayForAnother { get; set; }

        /// <summary>
        /// 订单赠送积分
        /// </summary>
        public int OrderGiveNum { get; set; }

        /// <summary>
        /// 扣分单自动分单？
        /// </summary>
        public int AutoOrderShunt { get; set; }

        /// <summary>
        /// 是否开通余额支付
        /// </summary>
        public int IsBalancePayment { get; set; }

        public int IsWeChatPayment { get; set; }

        public string WxappId { get; set; }

        public string WxmchId { get; set; }

        public string Wxkey { get; set; }

        public string WxsubAppid { get; set; }

        public string WxsubMchid { get; set; }

        public string WxsubKey { get; set; }

        public int WeiXinGiro { get; set; }
        public string WeiXinCodeUrl { get; set; }

        public int IsUseSysWeiXinPay { get; set; }

        /// <summary>
        /// 支付宝开放平台APPID
        /// </summary>
        public string AlipayAppId { get; set; }
        /// <summary>
        /// 支付宝开放平台RSA(SHA1)密钥公钥
        /// </summary>
        public string AlipayPublicKey { get; set; }
        /// <summary>
        /// 支付宝开放平台RSA(SHA1)密钥私钥
        /// </summary>
        public string AlipayPrivateKey { get; set; }
        /// <summary>
        /// 是否开通威富通支付
        /// </summary>
        public int IsWftPayment { get; set; }
        /// <summary>
        /// 威富通商户号
        /// </summary>
        public string WftMchId { get; set; }
        /// <summary>
        ///威富通接口key
        /// </summary>
        public string WftKey { get; set; }

        public int IsAlipayPayment { get; set; }

        public int AlipayGiro { get; set; }

        public string AlipaySellerEmail { get; set; }

        public string AlipayKey { get; set; }

        public string AlipayPartner { get; set; }

        public string AlipayCodeUrl { get; set; }

        public int IsUseSysAlipay { get; set; }
    }
}