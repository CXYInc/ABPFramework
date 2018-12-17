using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;

namespace CXY.CJS.WebApi.Configuration
{
    public class SmtpSenderConfiguration: ISmtpEmailSenderConfiguration, IEmailSenderConfiguration
    {
        public string DefaultFromAddress { get; set; }
        public string DefaultFromDisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}