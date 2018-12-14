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
        /// <summary>
        /// AbpSession
        /// </summary>
        public new IAbpSessionExtension AbpSession { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected CJSAppServiceBase()
        {
            LocalizationSourceName = CJSConsts.LocalizationSourceName;
        }
    }
}