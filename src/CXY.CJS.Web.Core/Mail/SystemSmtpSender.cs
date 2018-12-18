using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using CXY.CJS.Core.Utils.Mail;

namespace CXY.CJS.Web.Core.Mail
{
    public class SystemSmtpSender : SmtpSender, ISystemSmtpSender
    {
        private SystemSmtpSenderConfiguration _configuration;

        public SystemSmtpSender(SystemSmtpSenderConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(_configuration.Default, to, subject, body, isBodyHtml);
        }

        public async Task SendAsync(string fromName, string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendAsync(_configuration.Others.FirstOrDefault(i => i.Name == fromName), to, subject, body, isBodyHtml);
        }

        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(_configuration.Default, to, subject, body, isBodyHtml);
        }

        public void Send(string fromName, string to, string subject, string body, bool isBodyHtml = true)
        {
            Send(_configuration.Others.FirstOrDefault(i => i.Name == fromName), to, subject, body, isBodyHtml);
        }
    }
}