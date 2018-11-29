using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CXY.CJS
{
    [DependsOn(typeof(CJSCoreModule), typeof(AbpAutoMapperModule))]
    public class CJSApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSApplicationModule).GetAssembly());
        }
    }
}