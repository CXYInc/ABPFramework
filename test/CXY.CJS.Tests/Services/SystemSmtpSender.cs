using System.Threading.Tasks;
using CXY.CJS.Core.Utils.Mail;

namespace CXY.CJS.Tests.Services
{
    public class TestSystemSmtpSender : ISystemSmtpSender
    {
        public Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            return Task.CompletedTask;
        }

        public Task SendAsync(string fromName, string to, string subject, string body, bool isBodyHtml = true)
        {
            return Task.CompletedTask;
        }

        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {

        }

        public void Send(string fromName, string to, string subject, string body, bool isBodyHtml = true)
        {
        }
    }
}