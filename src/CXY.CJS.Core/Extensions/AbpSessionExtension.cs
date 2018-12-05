using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime;
using Abp.Runtime.Session;
using CXY.CJS.Constant;
using System.Linq;
using System.Security.Claims;

namespace CXY.CJS.Extensions
{
    class AbpSessionExtension : ClaimsAbpSession, IAbpSessionExtension
    {

        public AbpSessionExtension(IPrincipalAccessor principalAccessor, IMultiTenancyConfig multiTenancy, ITenantResolver tenantResolver, IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
        }

        public string WebSiteId => GetClaimValue(ClaimConst.WebSiteId);

        public new string UserId => GetClaimValue(ClaimConst.UserId);

        public  string UserName => GetClaimValue(ClaimConst.UserName);

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
