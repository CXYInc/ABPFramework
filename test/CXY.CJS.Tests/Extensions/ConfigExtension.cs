using System;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using CXY.CJS.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CXY.CJS.Tests.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddConfigModel(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(t => t.GetAttribute<ConfigModelAttribute>() != null)
                .ToArray();

            foreach (var type in types)
            {
                services.AddSingleton(type, provider =>
                {
                    return Activator.CreateInstance(type);
                });
            }


            return services;
        }
    }
}