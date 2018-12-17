using System;

namespace CXY.CJS.Tests.TestDatas
{
    public class RoleDatas
    {
        public static readonly Model.Role LowerAgentRole = new Model.Role
        {
            Id = Guid.NewGuid().ToString(),
            CreationTime = DateTime.Now,
            Name = "LowerAgentRole",
            DisplayName = "下级代理角色",
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
        };
    }
}