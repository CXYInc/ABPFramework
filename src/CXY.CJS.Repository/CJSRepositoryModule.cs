using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Core;

namespace CXY.CJS.Repository
{
    [DependsOn(typeof(CJSCoreModule))]
    public class CJSRepositoryModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSRepositoryModule).GetAssembly());
        }
    }
}
