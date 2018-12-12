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
using CXY.CJS.Config;
using CXY.CJS.Tests.Extensions;
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
            var services = new ServiceCollection()
                //.AddConfigModel()
                .AddEntityFrameworkSqlServer();
            //Configuration.UnitOfWork.IsTransactional = false;
            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
            SetupDb(serviceProvider);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSTestModule).GetAssembly());
        }

      

        private void SetupDb(IServiceProvider serviceProvider)
        {
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