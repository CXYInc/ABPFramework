using Abp.Application.Services;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Extensions;

namespace CXY.CJS
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CJSAppServiceBase : ApplicationService
    {
        //隐藏父类的AbpSession
        public new IAbpSessionExtension AbpSession { get; set; }

        protected CJSAppServiceBase()
        {
            LocalizationSourceName = CJSConsts.LocalizationSourceName;
        }
    }
}