using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.Web.Core
{
    [DependsOn(typeof(CJSApplicationModule), typeof(AbpAspNetCoreModule))]
    public class CJSWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CJSWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(CJSConsts.ConnectionStringName);

            Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnError = false;
            Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnSuccess = false;

            //GlobalConfiguration.Configuration.Formatters.Clear();
            //var formatter = new JsonMediaTypeFormatter();
            //formatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //formatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //GlobalConfiguration.Configuration.Formatters.Add(formatter);
            //GlobalConfiguration.Configuration.Formatters.Add(new PlainTextFormatter());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSWebCoreModule).GetAssembly());
        }
    }
}
