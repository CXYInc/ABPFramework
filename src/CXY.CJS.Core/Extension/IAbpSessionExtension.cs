using Abp.Runtime.Session;

namespace CXY.CJS.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAbpSessionExtension : IAbpSession
    {
        new string UserId { get; }

        string WebSiteId { get; }

        string UserName { get; }
    }
}
