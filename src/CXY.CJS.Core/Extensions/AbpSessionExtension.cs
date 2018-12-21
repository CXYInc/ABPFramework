using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using CXY.CJS.Core.Constant;
using System.Linq;

namespace CXY.CJS.Core.Extensions
{
    /// <summary>
    /// AbpSession扩展
    /// </summary>
    public class AbpSessionExtension : ClaimsAbpSession, IAbpSessionExtension
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="principalAccessor"></param>
        /// <param name="multiTenancy"></param>
        /// <param name="tenantResolver"></param>
        /// <param name="sessionOverrideScopeProvider"></param>
        public AbpSessionExtension(IPrincipalAccessor principalAccessor, IMultiTenancyConfig multiTenancy, ITenantResolver tenantResolver, IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
        }

        /// <summary>
        /// 当前站点ID
        /// </summary>
        public string WebSiteId => GetClaimValue(ClaimConst.WebSiteId);

        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public new string UserId => GetClaimValue(ClaimConst.UserId);

        /// <summary>
        /// 当前登录用户名称
        /// </summary>
        public string UserName => GetClaimValue(ClaimConst.UserName);

        private string GetClaimValue(string claimType)
        {
            var claimsPrincipal = PrincipalAccessor.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
            if (string.IsNullOrEmpty(claim?.Value))
                return null;

            return claim.Value;
        }
    }
}
