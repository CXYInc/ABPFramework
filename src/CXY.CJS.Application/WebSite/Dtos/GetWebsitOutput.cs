using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CXY.CJS.Application.Dtos
{
    public class GetWebsitOutput
    {
        public string Id { get; set; }
        public int IsBalancePayment { get; set; }
        public string AlipayCodeUrl { get; set; }
        public string loginname { get; set; }
        public decimal? ReceivableAmount { get; set; }
        public string WxappID { get; set; }
        public int SMSSendInterval { get; set; }
        public string ExpirationReminder { get; set; }
        public int DefaultCheckLock { get; set; }
        public int IsInvoice { get; set; }
        public string AskPriceMailAddress { get; set; }
        public int GivePointsPerMonth { get; set; }
        public string WFTKey { get; set; }
        [JsonProperty("Wxsub_appid")]
        public string WxsubAppId { get; set; }
        public string AlipayPrivateKey { get; set; }
        [JsonProperty("WFTMch_id")]
        public string WFTMchId { get; set; }
        public string WebSiteChName { get; set; }
        public string AlipayPublicKey { get; set; }
        public string AlipayAppID { get; set; }
        public string Email { get; set; }
        public string Copyright { get; set; }
        public int IsUseSysAlipay { get; set; }
        public int IsAlipayPayment { get; set; }
        public string EndTime { get; set; }
        public string WeiXinCodeUrl { get; set; }
        public string AlipayPartner { get; set; }
        public int OrderGiveNum { get; set; }
        public string WorkerName { get; set; }
        public string AlipaySellerEmail { get; set; }
        public string ConcernArea { get; set; }
        public string WebSiteType { get; set; }
        public int WeiXinGiro { get; set; }
        public int IsDownApp { get; set; }
        [JsonProperty("Wxsub_key")]
        public string WxsubKey { get; set; }
        public int AlipayGiro { get; set; }
        public string WebSiteMemo { get; set; }
        [JsonProperty("Wxsub_mch_id")]
        public string WxsubMchId { get; set; }
        public string Wxkey { get; set; }
        public string CustQQ { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? DefaultNotePrice { get; set; }
        public int IsWFTPayment { get; set; }
        public int FirstAgentDefaultGivePoints { get; set; }
        public string H5ImgAddTime { get; set; }
        public string Created { get; set; }
        public decimal? DefaultJFPrice { get; set; }
        public string SiteLoginImage { get; set; }
        public int IsWeChatPayment { get; set; }
        public int GivePointsSurplusSameMonth { get; set; }
        public decimal? WebFixedProfit { get; set; }
        public bool PayForAnother { get; set; }
        public int IsUseSysWeiXinPay { get; set; }
        public int VisibleCalculationExpression { get; set; }
        public string AlipayKey { get; set; }
        public string DefaultCarNumberForShort { get; set; }
        public decimal? QuickAmount { get; set; }
        public string WebSiteKey { get; set; }
        public string H5ImgUrl { get; set; }
        public string WebSiteMater { get; set; }
        public int MasterAgentDefaultGivePoints { get; set; }
        public string ReceivableDate { get; set; }

        [JsonProperty("PROVINCEID")]
        public string Provinceid { get; set; }
        public string WebSiteDomains { get; set; }
        [JsonProperty("Wxmch_id")]
        public string WxmchId { get; set; }
        public int IsRevice { get; set; }
        public string WebSiteId { get; set; }
        public int AutoOrderShunt { get; set; }
        public string CustSerPhone { get; set; }
        public int SecondAgentDefaultGivePoints { get; set; }

    }
}