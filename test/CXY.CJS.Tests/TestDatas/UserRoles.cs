using System;

namespace CXY.CJS.Tests.TestDatas
{
    public class UserRoles
    {
        public static readonly Model.UserRole LowerAgentTestRole = new Model.UserRole
        {
            Id =Guid.NewGuid().ToString(),
            CreationTime = DateTime.Now,
            RoleId = RoleDatas.LowerAgentRole.Id,
            UserId = UserDatas.SuperWebSiteLowerAgent.Id,
            WebSiteId = WebSiteDatas.SuperWebSite.Id
        };
    }
}