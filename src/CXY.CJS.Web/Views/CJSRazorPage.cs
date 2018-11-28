using Abp.AspNetCore.Mvc.Views;

namespace CXY.CJS.Web.Views
{
    public abstract class CJSRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected CJSRazorPage()
        {
            LocalizationSourceName = CJSConsts.LocalizationSourceName;
        }
    }
}
