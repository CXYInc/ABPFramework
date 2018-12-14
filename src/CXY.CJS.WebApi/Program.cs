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

            var app = config.GetValue<string>("App:ServerRootAddress");

            var urls = new string[] { "http://*:5000" };
            urls = app.Split(",");

            var builder = WebHost.CreateDefaultBuilder(args);

            builder.UseUrls(urls)
                   .UseStartup<Startup>();

            return builder;
        }
    }
}
