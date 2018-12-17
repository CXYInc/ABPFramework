using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Authorization;

namespace CXY.CJS.Tests.Services
{
    public class NotActiveAuthorizationHelper:IAuthorizationHelper
    {
        public Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
        {
            return Task.CompletedTask;
        }

        public Task AuthorizeAsync(MethodInfo methodInfo, Type type)
        {
            return Task.CompletedTask;
        }
    }
}