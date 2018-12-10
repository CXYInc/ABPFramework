using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Repository;

namespace CXY.CJS.EntityFrameworkCore
{
    [DependsOn(typeof(CJSCoreModule), typeof(AbpEntityFrameworkCoreModule),typeof(CJSRepositoryModule))]
    public class CJSEntityFrameworkCoreModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<CJSDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            //Configuration.ReplaceService<IEfCoreTransactionStrategy, DbContextEfCoreTransactionStrategy>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSEntityFrameworkCoreModule).GetAssembly());
        }
    }
}