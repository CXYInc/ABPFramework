using Abp.AspNetCore.Mvc.Controllers;

namespace CXY.CJS.Web.Controllers
{
    public abstract class CJSControllerBase: AbpController
    {
        protected CJSControllerBase()
        {
            LocalizationSourceName = CJSConsts.LocalizationSourceName;
        }
    }
}