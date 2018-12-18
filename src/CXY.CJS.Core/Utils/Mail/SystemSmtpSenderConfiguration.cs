using System.Collections.Generic;

namespace CXY.CJS.Core.Utils.Mail
{
    public class SystemSmtpSenderConfiguration
    {
        public SmtpConfiguration Default { get; set; }
        public List<SmtpConfiguration> Others { get; set; }
    }
}