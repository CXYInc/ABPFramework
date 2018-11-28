using Abp.Application.Services;

namespace CXY.CJS
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CJSAppServiceBase : ApplicationService
    {
        protected CJSAppServiceBase()
        {
            LocalizationSourceName = CJSConsts.LocalizationSourceName;
        }
    }
}