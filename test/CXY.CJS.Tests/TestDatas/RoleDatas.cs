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


        public static readonly Model.Role WillBeGrantOrRemoveRole = new Model.Role
        {
            Id = Guid.NewGuid().ToString(),
            CreationTime = DateTime.Now,
            Name = "WillBeGrantOrRemoveRole",
            DisplayName = "测试授予或移除用户的角色",
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
        };
    }
}