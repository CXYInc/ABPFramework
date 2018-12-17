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
        protected IEnumerable<string> GetUserInfo()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }
    }
}