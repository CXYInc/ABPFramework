namespace CXY.CJS.Core.Config
{
    /// <summary>
    /// JwtBearer配置实体
    /// </summary>
    public class JwtBearerConfig
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 签发人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Token过期时间(分钟)
        /// </summary>
        public int ValidMinutes { get; set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string PrivateKeys { get; set; }
    }
}
