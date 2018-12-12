using System;
using System.Linq;
using CXY.CJS.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CXY.CJS.Tests.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddConfigModel(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()
                    .Where(t => t.GetCustomAttributes(typeof(ConfigModelAttribute),false) != null))
                .ToArray();

            foreach (var type in types)
            {
                services.AddSingleton(type, provider =>
                {
                    return null;
                });
            }


            return services;
        }
    }
}