using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Repository;

namespace CXY.CJS
{
    [DependsOn(typeof(CJSCoreModule), typeof(CJSRepositoryModule), typeof(AbpAutoMapperModule))]
    public class CJSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CJSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            //AutoMapper注入
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg => cfg.AddProfiles(thisAssembly));
        }
    }
}