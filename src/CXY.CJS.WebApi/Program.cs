using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CXY.CJS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            //这里添加配置文件
                            .AddJsonFile("appsettings.json", true)
                            .AddCommandLine(args)
                            .Build();

            var serverRootAddress = config.GetValue<string>("App:ServerRootAddress");
            var maxRequestBodySize = config.GetValue<string>("App:MaxRequestBodySize");

            var urls = new string[] { "http://*:5000" };
            urls = serverRootAddress.Split(",");

            var builder = WebHost.CreateDefaultBuilder(args);

            builder.UseUrls(urls)
                   .UseStartup<Startup>()
                   .UseKestrel(options =>
                   {
                       if (!string.IsNullOrWhiteSpace(maxRequestBodySize) && long.TryParse(maxRequestBodySize, out long size))
                       {
                           options.Limits.MaxRequestBodySize = size;
                       }
                   });

            return builder;
        }
    }
}
