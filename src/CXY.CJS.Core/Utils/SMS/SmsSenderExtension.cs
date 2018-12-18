using Microsoft.Extensions.DependencyInjection;

namespace CXY.CJS.Core.Utils.SMS
{
    public static class SmsSenderExtension
    {
        public static IServiceCollection AddSmsSender(this IServiceCollection services, SmsSenderConfiguration configuration)
        {
            services.AddTransient<ISmsSender,SmsSender>();
            services.AddSingleton<SmsSenderConfiguration>(configuration);
            return services;
        }
    }
}