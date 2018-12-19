using System.Collections.Concurrent;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.Core.Configuration
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public static class AppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        static AppConfigurations()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IConfigurationRoot Get(string path, string environmentName = null)
        {
            var cacheKey = path + "#" + environmentName;
            return ConfigurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            //serilog配置
            builder.AddJsonFile("serilogsetting.json", optional: true, reloadOnChange: true);

            builder = builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
