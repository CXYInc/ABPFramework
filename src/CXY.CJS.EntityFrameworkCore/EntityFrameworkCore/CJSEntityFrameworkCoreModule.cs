using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using CXY.CJS.Configuration;
using CXY.CJS.Repository;
using CXY.CJS.Web;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CXY.CJS.EntityFrameworkCore
{
    [DependsOn(typeof(CJSCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class CJSEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CJSEntityFrameworkCoreModule).GetAssembly());
        }
    }
}