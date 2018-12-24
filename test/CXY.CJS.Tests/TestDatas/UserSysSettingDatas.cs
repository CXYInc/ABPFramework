using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public class UserSysSettingDatas
    {
        public static readonly UserSysSetting SuperWebSiteLowerAgentSysSetting = new UserSysSetting
        {
            Id=UserDatas.SuperWebSiteLowerAgent.Id,
            ParentId = UserDatas.SuperWebSiteMasterUser.Id,
            Swfzr = WebSiteDatas.SuperWebSite.WorkerName,
            ValidityDate = DateTime.Now,
            WebSiteId = UserDatas.SuperWebSiteLowerAgent.WebSiteId,
            Userlayer = "0",
            Rate = 1.5m,
            RateType = 1
        };
    }
}