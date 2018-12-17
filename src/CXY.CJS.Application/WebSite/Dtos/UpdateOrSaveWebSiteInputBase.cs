using System;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CXY.CJS.Application.Dtos
{
    public class UpdateOrSaveWebSiteInputBase: ICustomValidate
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [Required]
        public string WebSiteName { get; set; }

        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 订单前缀
        /// </summary>
        [Required]
        public string WebSiteKey { get; set; }

        public string WebSiteType { get; set; }

        /// <summary>
        /// 订单积分赠送
        /// </summary>
        [Required]
        public int OrderGiveNum { get; set; }
        public string PROVINCEID { get; set; }
        public string Email { get; set; }
        public string CustSerPhone { get; set; }
        public string CustQQ { get; set; }
        public int VisibleCalculationExpression { get; set; }
        public string DefaultCarNumberForShort { get; set; }
        public int GivePointsPerMonth { get; set; }

        /// <summary>
        /// 总站登录账号名字
        /// </summary>
        [Required]
        public string Loginname { get; set; }

        /// <summary>
        /// 总站代理兑换积分单价
        /// </summary>
        [Required]
        public decimal DefaultJfPrice { get; set; }

        /// <summary>
        /// 该总站代理兑换短信单价
        /// </summary>
        [Required]
        public decimal DefaultNotePrice { get; set; }
        public int IsRevice { get; set; }
        public string DefaultCheckLock { get; set; }
        public DateTime? EndTime { get; set; } = DateTime.Now.AddDays(10);
        public int GivePointsSurplusSameMonth { get; set; }
        public int MasterAgentDefaultGivePoints { get; set; }
        public int FirstAgentDefaultGivePoints { get; set; }
        public int SecondAgentDefaultGivePoints { get; set; }
        public int IsBalancePayment { get; set; }
        public int AutoOrderShunt { get; set; }
        public int AlipayGiro { get; set; }
        public string AlipayCodeUrl { get; set; }
        public int WeiXinGiro { get; set; }
        public string WeiXinCodeUrl { get; set; }
        public int IsAlipayPayment { get; set; }
        public int IsUseSysAlipay { get; set; }
        public string AlipaySellerEmail { get; set; }
        public string AlipayPartner { get; set; }
        public string AlipayKey { get; set; }
        public string AlipayAppID { get; set; }
        public string AlipayPublicKey { get; set; }
        public string AlipayPrivateKey { get; set; }
        public int IsWeChatPayment { get; set; }
        public int IsUseSysWeiXinPay { get; set; }
        public bool PayForAnother { get; set; }
        public string WxappID { get; set; }
        //[JsonProperty("Wxsub_appid")]
        public string WxsubAppId { get; set; }
        //[JsonProperty("Wxmch_id")]
        public string WxmchId { get; set; }
        //[JsonProperty("Wxsub_mch_id")]
        public string WxSubMchId { get; set; }
        public string Wxkey { get; set; }
        //[JsonProperty("Wxsub_key")]
        public string WxsubKey { get; set; }
        public int IsWFTPayment { get; set; }
        //[JsonProperty("WFTMch_id")]
        public string WFTMchId { get; set; }
        public string WFTKey { get; set; }
        /// <summary>
        /// 客户经理
        /// </summary>
        public string WorkerName { get; set; }
        public Decimal ReceivableAmount { get; set; }
        public DateTime ReceivableDate { get; set; }
        public string WebSiteDomains { get; set; }
        public string WebSiteMemo { get; set; }
        public string Copyright { get; set; }
        public string SiteLoginImage { get; set; }

        public virtual void AddValidationErrors(CustomValidationContext context)
        {
            if (Id.Length!=6)
            {
                context.Results.Add(new ValidationResult("站点Id必须为6位"));
            }

            if (IsAlipayPayment ==1 && IsUseSysAlipay != 1)
            {
                // 开通支付宝功能，并且使用的是企业，那么必须填写支付宝参数
                if (string.IsNullOrEmpty(AlipayKey) || string.IsNullOrEmpty(AlipayPartner) || string.IsNullOrEmpty(AlipaySellerEmail))
                {
                    context.Results.Add(new ValidationResult("开通 企业支付宝 的情况下必须完整填写 支付宝参数"));
                }
            }

            if (IsWeChatPayment == 1 && IsUseSysWeiXinPay != 1)
            {
                // 开通微信功能，并且使用的是企业，那么必须填写微信参数
                if (string.IsNullOrEmpty(WxappID) || string.IsNullOrEmpty(WxmchId) || string.IsNullOrEmpty(Wxkey))
                {
                    context.Results.Add(new ValidationResult("开通 企业微信 的情况下必须完整填写 微信参数"));
                }
            }

            if (IsWFTPayment == 1)
            {
                // 开通威富通支付，那么必须填写威富通参数
                if (string.IsNullOrEmpty(WFTMchId) || string.IsNullOrEmpty(WFTKey))
                {
                    context.Results.Add(new ValidationResult("开通 威富通支付 的情况下必须完整填写 威富通支付参数"));
                }
            }


        }
    }

    public class UpdateWebSiteInput : UpdateOrSaveWebSiteInputBase
    {
        [Required]
        public string WebSiteMater { get; set; }

        public override void AddValidationErrors(CustomValidationContext context)
        {
            base.AddValidationErrors(context);
        }
    }
    public class SaveWebSiteInput : UpdateOrSaveWebSiteInputBase
    {
        
    }
}