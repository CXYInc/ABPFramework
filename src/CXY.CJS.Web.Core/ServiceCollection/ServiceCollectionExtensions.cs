using CXY.CJS.Web.Core.Filter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Text;
using CXY.CJS.Core.Config;
using CXY.CJS.Web.Core.Extensions;
using MediatR;

namespace CXY.CJS.Web.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebApiServices(this IServiceCollection services, IHostingEnvironment env)
        {
            //使程序支持GBK,gb2312
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //services.AddConfigModel();

            services.AddHttpClient();
            services.AddConfigModel(typeof(ConfigModelAttribute).Assembly);
            services.AddMvc(config =>
            {
                config.Filters.Add(new ApiErrorAttibute());
            });

            services.PostConfigure<MvcJsonOptions>(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddTransient<IMediator, Mediator>();


        }
    }
}
