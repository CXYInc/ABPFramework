using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public static class MenuDatas
    {
        public static readonly Model.Menu UserModule = new Model.Menu
        {
            Id = "1",
            ParentId = "0",
            MenuName = "用户模块",
            MenuLeval = 1,
            MenuLayer = 1000000000,
            TargetFrame = "#",
            MenuUrl = "#",
            IsOut = false,
            Weight = 0,
            IsDeleted = false,
            IsParent = true,
            IsSys = true,
            LastModificationTime = DateTime.Now
        };

        public static readonly Model.Menu DedeletedModuleMenu = new Model.Menu
        {
            Id = Guid.NewGuid().ToString().Substring(0, 4),
            ParentId = "0",
            MenuName = "已被删除的菜单",
            MenuLeval = 1,
            MenuLayer = 1000000000,
            TargetFrame = "#",
            MenuUrl = "#",
            IsOut = false,
            Weight = 0,
            IsDeleted = true,
            IsParent = true,
            IsSys = true,
            LastModificationTime = DateTime.Now
        };

        public static readonly Model.Menu WillBeDedeletedMenu= new Model.Menu
        {
            Id = Guid.NewGuid().ToString().Substring(0, 6),
            ParentId = "0",
            MenuName = "将被被删除的菜单",
            MenuLeval = 1,
            MenuLayer = 1000000000,
            TargetFrame = "#",
            MenuUrl = "#",
            IsOut = false,
            Weight = 0,
            IsDeleted = false,
            IsParent = true,
            IsSys = true,
            LastModificationTime = DateTime.Now
        };
    }
}