using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.HttpClient;
using CXY.CJS.Localization;

namespace CXY.CJS
{
    public class CJSCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            CJSLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSCoreModule).GetAssembly());
            IocManager.Register<HttpClientHelper>(DependencyLifeStyle.Singleton);
        }
    }
}