namespace CXY.CJS.Core.Utils.Mail
{
    public class SmtpConfiguration
    {
        public string Name { get; set; }
        /// <summary>
        /// 邮件服务器主机
        /// </summary>
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 是否启用ssl
        /// </summary>
        public bool EnableSsl { get; set; }
    }
}