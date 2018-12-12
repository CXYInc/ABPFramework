using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CXY.CJS.Application.Dtos
{
    public class UpdateOrSaveWebSiteInputBase
    {
        public string WebSiteChName { get; set; }

        [Required]
        public string WebSiteId { get; set; }
      
        public string WebSiteKey { get; set; }
        public string WebSiteType { get; set; }
        public int OrderGiveNum { get; set; }
        public string PROVINCEID { get; set; }
        public string Email { get; set; }
        public string CustSerPhone { get; set; }
        public string CustQQ { get; set; }
        public int VisibleCalculationExpression { get; set; }
        public string DefaultCarNumberForShort { get; set; }
        public int GivePointsPerMonth { get; set; }
        public string Loginname { get; set; }
        public decimal DefaultJfPrice { get; set; }
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
        [JsonProperty("Wxsub_appid")]
        public string WxsubAppId { get; set; }
        [JsonProperty("Wxmch_id")]
        public string WxmchId { get; set; }
        public string Wxsub_mch_id { get; set; }
        public string Wxkey { get; set; }
        [JsonProperty("Wxsub_key")]
        public string WxsubKey { get; set; }
        public int IsWFTPayment { get; set; }
        [JsonProperty("WFTMch_id")]
        public string WFTMchId { get; set; }
        public string WFTKey { get; set; }
        public string WorkerName { get; set; }
        public Decimal ReceivableAmount { get; set; }
        public DateTime ReceivableDate { get; set; }
        public string WebSiteDomains { get; set; }
        public string WebSiteMemo { get; set; }
        public string Copyright { get; set; }
        public string SiteLoginImage { get; set; }

    }

    public class UpdateWebSiteInput : UpdateOrSaveWebSiteInputBase
    {
        public string Id { get; set; }
        public string WebSiteMater { get; set; }
    }
    public class SaveWebSiteInput : UpdateOrSaveWebSiteInputBase
    {
        
    }
}