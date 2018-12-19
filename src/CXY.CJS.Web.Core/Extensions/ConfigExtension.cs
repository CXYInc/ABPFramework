using System;
using System.Linq;
using System.Reflection;
using CXY.CJS.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CXY.CJS.Web.Core.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddConfigModel(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes().Where(t => t.GetCustomAttributes(typeof(ConfigModelAttribute)) != null))
                                .ToArray();

            foreach (var type in types)
            {
                services.AddSingleton(type, provider =>
                {
                    var config = provider.GetService<IConfiguration>().GetSection(type.Name);
                    return config.Get(type);
                });
            }


            return services;
        }
    }
}