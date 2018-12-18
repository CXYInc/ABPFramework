using CXY.CJS.Core.Utils.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace CXY.CJS.Web.Core.Mail
{
    public static class SmtpSenderExtension
    {
        public static IServiceCollection AddSystemSmtpSender(this IServiceCollection services, SystemSmtpSenderConfiguration configuration)
        {
            services.AddSingleton<SystemSmtpSenderConfiguration>(configuration);
            services.AddTransient<ISystemSmtpSender,SystemSmtpSender>();
            return services;
        }
    }
}