using Castle.Core.Internal;
using CXY.CJS.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace CXY.CJS.Web.Core.Extensions
{
    public static class ConfigExtension
    {
        public static IServiceCollection AddConfigModel(this IServiceCollection services, Assembly assembly)
        {
            //todo:区分重载
            var types = assembly.GetTypes().Where(t =>t.GetAttribute<ConfigModelAttribute>()!=null)
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