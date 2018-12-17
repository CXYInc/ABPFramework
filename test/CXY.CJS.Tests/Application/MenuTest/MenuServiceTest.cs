﻿using Abp.AutoMapper;
using Abp.UI;
using CXY.CJS.Menu;
using CXY.CJS.Tests.TestDatas;
using System;
using System.Linq;
using System.Threading.Tasks;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using Xunit;

namespace CXY.CJS.Tests.Application.MenuTest
{
    public class MenuServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IMenuService _service;
        public MenuServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IMenuService>();
        }


        [Fact]
        public async Task GetMenu_When_NotFund()
        {
            var noExistResult = await _service.GetMenu(Guid.NewGuid().ToString() + "1234");
            Assert.Null(noExistResult);

            var deletedListResult = await _service.GetMenu(MenuDatas.DedeletedModuleMenu.Id);
            Assert.Null(deletedListResult);
        }


        [Fact]
        public async Task GetMenu_When_Fund()
        {
            var userModulItem = await _service.GetMenu(MenuDatas.UserModule.Id);
            Assert.NotNull(userModulItem);
        }

        [Fact]
        public async Task GetMenuList_When_NotFund()
        {
            var thanCountResult = await _service.ListMenu(new ListMenuInput
            {
                PageIndex = 100,
                PageSize = 10
            });
            Assert.Empty(thanCountResult.Datas);
        }


        [Fact]
        public async Task GetMenuList_When_Found()
        {
            var noFilterResult = await _service.ListMenu(new ListMenuInput
            {
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(noFilterResult.Datas);
        }

        [Fact]
        public async Task SaveMenu_When_Success()
        {
            var insertOutput = MenuDatas.UserModule.MapTo<SaveMenuInput>();
            insertOutput.MenuName = "子菜单";
            insertOutput.ParentId = MenuDatas.UserModule.Id;
            await _service.SaveMenu(insertOutput);
            var afterMenu =
                (await _service.ListMenu(new ListMenuInput()))
                .Datas.FirstOrDefault(i => i.MenuName == "子菜单");
            Assert.Equal(afterMenu.MenuLeval, MenuDatas.UserModule.MenuLeval+1);
        }


        [Fact]
        public async Task SaveMenu_When_Fail()
        {
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                var insertOutput = MenuDatas.UserModule.MapTo<SaveMenuInput>();
                insertOutput.ParentId = Guid.NewGuid().ToString();
                await _service.SaveMenu(insertOutput);
            });
        }

        [Fact]
        public async Task UpdateMenu_When_NotFound()
        {
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                var updateOutput = MenuDatas.UserModule.MapTo<UpdateMenuInput>();
                updateOutput.Id = Guid.NewGuid().ToString();
                await _service.UpdateMenu(updateOutput);
            });

        }

        [Fact]
        public async Task UpdateMenu_When_Success()
        {
            var newUserModule = MenuDatas.UserModule;
            newUserModule.IsOut = true;
            await _service.UpdateMenu(newUserModule.MapTo<UpdateMenuInput>());
            var finalUserModule = await _service.GetMenu(newUserModule.Id);
            Assert.Equal(newUserModule.IsOut, finalUserModule.IsOut);
        }

        [Fact]
        public async Task RemoveMenu_When_NotFound()
        {
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                Assert.True(await _service.RemoveMenu(Guid.NewGuid().ToString()));
            });
        }


        [Fact]
        public async Task RemoveMenu_When_Success()
        {
            Assert.True(await _service.RemoveMenu(MenuDatas.WillBeDedeletedMenu.Id));
        }


    }
}