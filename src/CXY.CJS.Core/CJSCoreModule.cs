using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Core.Localization;

namespace CXY.CJS.Core
{
    /// <summary>
    /// CJSCoreModule
    /// </summary>
    public class CJSCoreModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            CJSLocalizationConfigurer.Configure(Configuration.Localization);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSCoreModule).GetAssembly());
            IocManager.Register<HttpClientHelper>(DependencyLifeStyle.Singleton);
        }
    }
}