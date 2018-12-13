using System;
using System.Threading.Tasks;
using CXY.CJS.Menu;
using CXY.CJS.Menu.Dto;
using CXY.CJS.Role;
using CXY.CJS.Tests.TestDatas;
using Xunit;

namespace CXY.CJS.Tests.Application.Menu
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

            var deletedListResult = await _service.GetMenu(MenuDatas.DedeletedModule.Id);
            Assert.Null(deletedListResult);
        }


        [Fact]
        public async Task GetMenu_When_Fund()
        {
            var userModulItem = await _service.GetMenu(MenuDatas.UserModule.Id);
            Assert.NotNull(userModulItem);
        }

        [Fact]
        public async Task GetWebSiteList_When_NotFund()
        {
            var thanCountResult = await _service.ListMenu(new ListMenuInput
            {
                PageIndex = 100,
                PageSize = 10
            });
            Assert.Empty(thanCountResult.Datas);
        }


        [Fact]
        public async Task GetWebSiteList_When_Found()
        {
            var noFilterResult = await _service.ListMenu(new ListMenuInput
            {
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(noFilterResult.Datas);
        }


    }
}