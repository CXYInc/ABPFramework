using System;
using System.Reflection;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.TestBase;
using CXY.CJS.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using CXY.CJS.Core.Config;
using CXY.CJS.Tests.Extensions;
using CXY.CJS.Tests.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            var thisAssembly = typeof(CJSTestModule).GetAssembly();

            var services = new ServiceCollection()
                //.AddConfigModel()
                .AddEntityFrameworkSqlServer();
            //Configuration.UnitOfWork.IsTransactional = false;

            // no active AuthorizationService
            NotUseAuthorizationService(services);

             var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
            
            //AutoMapper注入
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddProfiles(thisAssembly);
                //空值不进行Mapper
                cfg.ForAllMaps((obj, cnfg) => cnfg.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)));
            });

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

        private void NotUseAuthorizationService(IServiceCollection services)
        {
            services.Replace(Microsoft.Extensions.DependencyInjection.ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService>());
            services.Replace(Microsoft.Extensions.DependencyInjection.ServiceDescriptor.Singleton<IAuthorizationHelper, NotActiveAuthorizationHelper>());
        }
    }
}