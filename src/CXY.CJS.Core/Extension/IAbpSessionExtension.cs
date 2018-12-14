using Abp.Runtime.Session;

namespace CXY.CJS.Core.Extensions
{
    /// <summary>
    /// AbpSession扩展
    /// </summary>
    public interface IAbpSessionExtension : IAbpSession
    {
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        new string UserId { get; }

        /// <summary>
        /// 当前站点ID
        /// </summary>
        string WebSiteId { get; }

        /// <summary>
        /// 当前登录用户名称
        /// </summary>
        string UserName { get; }
    }
}
