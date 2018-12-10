﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace CXY.CJS.Web.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebApiServices(this IServiceCollection services, IHostingEnvironment env)
        {
            // 使程序支持GBK,gb2312
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddHttpClient();

            services.AddMvc();

            services.PostConfigure<MvcJsonOptions>(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }
    }
}