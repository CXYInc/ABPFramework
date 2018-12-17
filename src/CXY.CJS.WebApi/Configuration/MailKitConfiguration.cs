using Abp.MailKit;
using MailKit.Security;

namespace CXY.CJS.WebApi.Configuration
{
    public class MailKitConfiguration: IAbpMailKitConfiguration
    {
        public SecureSocketOptions? SecureSocketOption { get; set; } = SecureSocketOptions.Auto;
    }
}