using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Configuration;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.Web.Startup
{
    [DependsOn(
        typeof(CJSApplicationModule), 
        typeof(CJSEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class CJSWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public CJSWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(CJSConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<CJSNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(CJSApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSWebModule).GetAssembly());
        }
    }
}