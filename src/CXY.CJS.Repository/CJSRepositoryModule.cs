using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
