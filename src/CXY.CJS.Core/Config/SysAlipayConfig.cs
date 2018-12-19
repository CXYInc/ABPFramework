namespace CXY.CJS.Core.Config
{
    /// <summary>
    /// 支付宝支付配置
    /// </summary>
    [ConfigModel]
    public class SysAlipayConfig
    {
        public string AlipayKey { get; set; }
        public string AlipayPartner { get; set; }
        public string AlipaySellerEmail { get; set; }
        public string AlipayAppID { get; set; }
        public string AlipayPrivateKey { get; set; }
        public string AlipayPublicKey { get; set; }
    }
}