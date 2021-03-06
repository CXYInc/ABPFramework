﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Repository;
using CXY.CJS.Tests.TestDatas;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CXY.CJS.Tests.Application.UserTest
{
    public class UserServicesTest : IClassFixture<CJSTestBase>
    {
        private readonly IUserServices _service;

        public UserServicesTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IUserServices>();
        }


        [Fact]
        public async Task GetUser_When_Fund()
        {
            var user = await _service.Get(UserDatas.SuperWebSiteLowerAgent.Id);
            Assert.NotNull(user.Data);
        }


        [Fact]
        public async Task UpdateUser_When_Success()
        {
            var userResult = await _service.Get(UserDatas.SuperWebSiteLowerAgent.Id);
            var updateUser = userResult.Data.MapTo<UserEditInputDto>();
            var result = await _service.Update(updateUser);
            Assert.Equal(1, result.Code);
        }


        [Fact]
        public async Task DelUser_When_Success()
        {
            var result = await _service.Delete(UserDatas.WillBeDelUser.Id);
            Assert.Equal(1, result.Code);
        }


        [Fact]
        public async Task GetLowerAgentList_When_NotFund()
        {
            var thanCountResult = await _service.ListLowerAgent(new ListLowerAgentInput
            {
                Id = Guid.NewGuid().ToString(),
                WebSiteId = Guid.NewGuid().ToString(),
                PageSize = 100,
                PageIndex = 100,
            });
            Assert.Empty(thanCountResult.Data.Datas);
        }


        [Fact]
        public async Task GetLowerAgentList_When_Fund()
        {
            var result = await _service.ListLowerAgent(new ListLowerAgentInput
            {
                Id = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.ParentId,
                WebSiteId = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.WebSiteId,
                Swfzr = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.Swfzr,
                Start = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.ValidityDate.Value.AddYears(-1),
                End = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.ValidityDate.Value.AddDays(2),
                Key = UserDatas.SuperWebSiteLowerAgent.LoginName,
                MaxWdye = UserWalletDatas.SuperWebSiteLowerAgentUserWallet.Balance,
                MinWdye = UserWalletDatas.SuperWebSiteLowerAgentUserWallet.Balance,
                PageSize = 10,
                PageIndex = 1,
            });
            var agent = result.Data.Datas?.FirstOrDefault(i => i.Id == UserDatas.SuperWebSiteLowerAgent.Id);
            Assert.NotNull(agent);
        }

        [Fact]
        public async Task GetLowerAgent_When_NotFund()
        {
            var noExistResult = await _service.GetLowerAgent(Guid.NewGuid().ToString() + "1234");
            Assert.Equal(0, noExistResult.Code);
        }

        [Fact]
        public async Task GetLowerAgent_When_Fund()
        {
            var result = await _service.GetLowerAgent(UserDatas.SuperWebSiteLowerAgent.Id);
            Assert.Equal(result.Data.Id, UserDatas.SuperWebSiteLowerAgent.Id);
        }


        [Fact]
        public async Task GetUserRoles_When_Fund()
        {
            var result = await _service.GetUserRoles(UserDatas.SuperWebSiteLowerAgent.Id);
            Assert.NotEmpty(result.Data);
        }



        [Fact]
        public async Task GrantOrRemoveUserRole_When_NoFoundUserOrRole()
        {
            var nofoundUser = await _service.GrantOrRemoveUserRole(new GrantOrRemoveUserRoleInput
            {
                UserId = Guid.NewGuid().ToString(),
                RoleId = RoleDatas.WillBeGrantOrRemoveRole.Id,
            });
            Assert.Equal("无法找到该用户！", nofoundUser.Message);

            var nofoundRole = await _service.GrantOrRemoveUserRole(new GrantOrRemoveUserRoleInput
            {
                UserId = UserDatas.SuperWebSiteLowerAgent.Id,
                RoleId = Guid.NewGuid().ToString()
            });
            Assert.Equal("无法找到该角色！", nofoundRole.Message);
        }


        [Fact]
        public async Task GrantOrRemoveUserRole_When_Success()
        {
            var userId = UserDatas.SuperWebSiteLowerAgent.Id;
            var roleId = RoleDatas.WillBeGrantOrRemoveRole.Id;
            var grantResult = await _service.GrantOrRemoveUserRole(new GrantOrRemoveUserRoleInput
            {
                UserId = userId,
                RoleId = roleId,
                IsGrant = true
            });

            Assert.Equal(1, grantResult.Code);
            var afterGrant = await _service.GetUserRoles(userId);

            Assert.NotNull(afterGrant.Data.FirstOrDefault(i => i.RoleId == roleId));

            var removeResult = await _service.GrantOrRemoveUserRole(new GrantOrRemoveUserRoleInput
            {
                UserId = userId,
                RoleId = roleId,
                IsGrant = false
            });

            Assert.Equal(1, removeResult.Code);
            var afterRemove = await _service.GetUserRoles(userId);
            Assert.Null(afterRemove.Data.FirstOrDefault(i => i.RoleId == roleId));
        }



    }
}