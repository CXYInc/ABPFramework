using System.Collections.Generic;
using CXY.CJS.Core.Config;

namespace CXY.CJS.Core.Utils.Mail
{
    [ConfigModel]
    public class SystemSmtpSenderConfiguration
    {
        public SmtpConfiguration Default { get; set; }
        public List<SmtpConfiguration> Others { get; set; }
    }
}