using System;
using System.Reflection;
using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.TestBase;
using CXY.CJS.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CXY.CJS.Tests
{
    [DependsOn(typeof(CJSApplicationModule), typeof(CJSEntityFrameworkCoreModule), typeof(AbpTestBaseModule))]
    public class CJSTestModule : AbpModule
    {
      

        public CJSTestModule(CJSEntityFrameworkCoreModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        }

        public override void PreInitialize()
        {
            //Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
            SetupInMemoryDb();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSTestModule).GetAssembly());
        }

      

        private void SetupInMemoryDb()
        {
            var services = new ServiceCollection()
                .AddEntityFrameworkSqlServer();
          
            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);

            var builder = new DbContextOptionsBuilder<CJSDbContext>();


            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=monsters_{Guid.NewGuid().ToString()};Trusted_Connection=True;MultipleActiveResultSets=true")
                .UseInternalServiceProvider(serviceProvider);


          
            IocManager.IocContainer.Register(
                Component
                    .For<DbContextOptions<CJSDbContext>>()
                    .Instance(builder.Options)
                    .LifestyleSingleton()
            );
        }
    }
}