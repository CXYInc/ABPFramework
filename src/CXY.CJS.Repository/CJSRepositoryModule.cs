using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.EntityFrameworkCore;

namespace CXY.CJS.Repository
{
    [DependsOn(typeof(CJSCoreModule), typeof(CJSEntityFrameworkCoreModule))]
    public class CJSRepositoryModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSRepositoryModule).GetAssembly());
        }
    }
}
