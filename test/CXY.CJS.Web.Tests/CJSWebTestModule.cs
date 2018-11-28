using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Web.Startup;
namespace CXY.CJS.Web.Tests
{
    [DependsOn(
        typeof(CJSWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class CJSWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSWebTestModule).GetAssembly());
        }
    }
}