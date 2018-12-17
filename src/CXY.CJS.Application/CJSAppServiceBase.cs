using Abp.Application.Services;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Extensions;
using System.Collections.Generic;

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
        protected IEnumerable<string> GetUserInfo()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }
    }
}