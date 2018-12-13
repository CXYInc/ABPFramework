namespace CXY.CJS.Core.Config
{
    [ConfigModel]
    public class SysWeiXinPayConfig
    {
        public string WxAppId { get; set; }
        public string WxMchId { get; set; }
        public string WxKey { get; set; }
        public string WxSubAppId { get; set; }
        public string WxSubMchId { get; set; }
        public string WxSubKey { get; set; }
    }
}