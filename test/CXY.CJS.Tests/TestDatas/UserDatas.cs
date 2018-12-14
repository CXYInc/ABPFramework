using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public static class UserDatas
    {
        public static readonly User SuperWebSiteMasterUser=new User
        {
            Id = WebSiteDatas.SuperWebSite.WebSiteMater,
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            UserName = "robot",
            LoginName = "robot",
            Password = "e10adc3949ba59abbe56e057f20f883e",
            RealName = "超级管理员",
            PhoneNumber = "pALjSAftFS1wWos2Y6ZLeg==",
            Shortname = "小粽仔[9000]",
            FullName = "1",
            Ispanuse = 0,
            RecommendUserid = "0",
            CardNo = "450821198508281479",
            IsDeleted = false,
            IsPaymentPwd = 0
        };


        public static readonly User SuperWebSiteLowerAgent = new User
        {
            Id = Guid.NewGuid().ToString(),
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            UserName = "lowerAgent",
            LoginName = "lowerAgent",
            Password = "e10adc3949ba59abbe56e057f20f883e",
            RealName = "下级代理",
            PhoneNumber = "pALjSAftFS1wWos2Y6ZLeg==",
            Shortname = "下级代理",
            FullName = "下级代理",
            Ispanuse = 0,
            RecommendUserid = "0",
            CardNo = "450821198508281479",
            IsDeleted = false,
            IsPaymentPwd = 0
        };


        public static readonly User WillBeDelUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            WebSiteId = WebSiteDatas.SuperWebSite.Id,
            UserName = "lowerAgent",
            LoginName = "lowerAgent",
            Password = "e10adc3949ba59abbe56e057f20f883e",
            RealName = "下级代理",
            PhoneNumber = "pALjSAftFS1wWos2Y6ZLeg==",
            Shortname = "下级代理",
            FullName = "下级代理",
            Ispanuse = 0,
            RecommendUserid = "0",
            CardNo = "450821198508281479",
            IsDeleted = false,
            IsPaymentPwd = 0
        };
    }
}