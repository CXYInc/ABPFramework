using System.Threading.Tasks;
using Abp.Dependency;
using CXY.CJS.Core.Utils.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CXY.CJS.Web.Core.Mail
{
    public class SmtpSender : ISmtpSender, ITransientDependency
    {

        public async Task SendAsync(SmtpConfiguration config, string to, string subject, string body, bool isBodyHtml = true)
        {
            using (var client = Build(config))
            {
                var message = BuildMimeMessage(config.UserName, to, subject, body, isBodyHtml);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public void Send(SmtpConfiguration config, string to, string subject, string body, bool isBodyHtml = true)
        {
            using (var client = Build(config))
            {
                var message = BuildMimeMessage(config.UserName, to, subject, body, isBodyHtml);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public async Task SendAsync(string server, string toMail, string fromMail, string subject, string emailBody, string username,
            string password, int port, bool sslEnable, bool isBodyHtml = true)
        {
            var config = new SmtpConfiguration
            {
                Host = server,
                EnableSsl = sslEnable,
                Password = password,
                Port = port,
                UserName = fromMail
            };
            using (var client = Build(config))
            {
                var message = BuildMimeMessage(config.UserName, toMail, subject, emailBody, isBodyHtml);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public void Send(string server, string toMail, string fromMail, string subject, string emailBody, string username,
            string password, int port, bool sslEnable, bool isBodyHtml = true)
        {
            var config = new SmtpConfiguration
            {
                Host = server,
                EnableSsl = sslEnable,
                Password = password,
                Port = port,
                UserName = fromMail
            };
            using (var client = Build(config))
            {
                var message = BuildMimeMessage(config.UserName, toMail, subject, emailBody, isBodyHtml);
                client.Send(message);
                client.Disconnect(true);
            }
        }


        public virtual SmtpClient Build(SmtpConfiguration config)
        {
            var client = new SmtpClient();

            try
            {
                ConfigureClient(client, config);
                return client;
            }
            catch
            {
                client.Dispose();
                throw;
            }
        }

        protected virtual void ConfigureClient(SmtpClient client, SmtpConfiguration config)
        {
            client.Connect(
                config.Host,
                config.Port,
                GetSecureSocketOption(config.EnableSsl)
            );

            client.Authenticate(
                config.UserName,
                config.Password
            );
        }

        protected virtual SecureSocketOptions GetSecureSocketOption(bool enableSsl)
        {
            return enableSsl
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTlsWhenAvailable;
        }


        private static MimeMessage BuildMimeMessage(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            var bodyType = isBodyHtml ? "html" : "plain";
            var message = new MimeMessage
            {
                Subject = subject,
                Body = new TextPart(bodyType)
                {
                    Text = body
                }
            };

            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));

            return message;
        }
    }
}