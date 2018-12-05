using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Repository;

namespace CXY.CJS
{
    [DependsOn(typeof(CJSCoreModule), typeof(CJSRepositoryModule), typeof(AbpAutoMapperModule))]
    public class CJSApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSApplicationModule).GetAssembly());
        }
    }
}