using System;
using System.Threading.Tasks;
using Abp.UI;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Tests.TestDatas;
using Xunit;

namespace CXY.CJS.Tests.Application.WebSite
{
    public class WebSiteApplicationServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IWebSiteAppService _service;

        private static SaveWebSiteInput NewSaveWebSiteInput = new SaveWebSiteInput
        {
            WebSiteId = Guid.NewGuid().ToString("N").Substring(0, 6),
            WebSiteKey = Guid.NewGuid().ToString("N").Substring(0, 6),
        };

        public WebSiteApplicationServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IWebSiteAppService>();
        }


        [Fact]
        public async Task GetWebSite_When_NotFund()
        {
            var result = await _service.GetWebSite(Guid.NewGuid().ToString() + "1234");
            Assert.Null(result);
        }

        [Fact]
        public async Task SaveWebSite_When_CheckInput()
        {
            var full = WebSiteFull.MapFrom(NewSaveWebSiteInput);
            Assert.NotNull(full.WebSite);
        }

        [Fact]
        public async Task SaveWebSite_When_Existed_WebSiteId_Or_WebSiteKey()
        {
            // Existed WebSiteId
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _service.SaveWebSite(new SaveWebSiteInput
                {
                    WebSiteId = WebSiteDatas.SuperWebSite.Id
                });
            });
            // Existed WebSiteKey
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _service.SaveWebSite(new SaveWebSiteInput
                {
                    WebSiteId = Guid.NewGuid().ToString(),
                    WebSiteKey = WebSiteDatas.SuperWebSite.WebSiteKey
                });
            });
        }

        //[Fact]
        //public async Task SaveWebSite_When_Add_WebSiteMater()
        //{

        //}

        [Fact]
        public async Task SaveWebSite_When_Success()
        {
            var output = await _service.SaveWebSite(NewSaveWebSiteInput);
            Assert.NotEmpty(output.Safepassword);
        }

        [Fact]
        public async Task UpdateWebSite_When_Success()
        {

        }
    }
}