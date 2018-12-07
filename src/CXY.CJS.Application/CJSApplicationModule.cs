using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CXY.CJS.Repository;

namespace CXY.CJS
{
    [DependsOn(typeof(CJSCoreModule), typeof(CJSRepositoryModule), typeof(AbpAutoMapperModule))]
    public class CJSApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(CJSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            //AutoMapper注入
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddProfiles(thisAssembly);

                //空值不进行Mapper
                cfg.ForAllMaps((obj, cnfg) => cnfg.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)));
            });
        }
    }
}