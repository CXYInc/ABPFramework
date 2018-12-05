using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Extensions
{
    public interface IAbpSessionExtension : IAbpSession
    {
        new string UserId { get; }

        string WebSiteId { get; }

        string UserName { get; }
    }
}
