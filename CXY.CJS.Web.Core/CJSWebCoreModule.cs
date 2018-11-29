using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Configuration;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.Web.Core
{
    [DependsOn(typeof(CJSApplicationModule), typeof(CJSEntityFrameworkCoreModule), typeof(AbpAspNetCoreModule))]
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

            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(CJSApplicationModule).GetAssembly(), "App");
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSWebCoreModule).GetAssembly());
        }
    }
}
