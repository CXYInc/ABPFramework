using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Configuration;
using CXY.CJS.Web.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.WebApi
{
    [DependsOn(typeof(CJSWebCoreModule))]
    public class CJSWebApiModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CJSWebApiModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSWebApiModule).GetAssembly());
        }
    }
}
