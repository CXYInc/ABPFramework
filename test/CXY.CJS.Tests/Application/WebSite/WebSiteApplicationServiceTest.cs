using System;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Tests.TestDatas;
using Newtonsoft.Json.Linq;
using Xunit;

namespace CXY.CJS.Tests.Application.WebSite
{
    public class WebSiteApplicationServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IWebSiteAppService _service;

       

        public WebSiteApplicationServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IWebSiteAppService>();
        }


        [Fact]
        public async Task GetWebSite_When_Fund()
        {
            var first = await _service.GetWebSite(WebSiteDatas.SuperWebSite.Id);
            Assert.NotNull(first);

            var second = await _service.GetWebSite(WebSiteDatas.SuperWebSite.Id);
            Assert.NotNull(second);
        }


        [Fact]
        public async Task GetWebSite_When_NotFund()
        {
            var noExistResult = await _service.GetWebSite(Guid.NewGuid().ToString() + "1234");
            Assert.Null(noExistResult.Data);

            var deletedListResult = await _service.GetWebSite(WebSiteDatas.DedeletedWebSite.Id);
            Assert.Null(deletedListResult.Data);
        }


        [Fact]
        public async Task GetWebSiteList_When_NotFund()
        {
            var thanCountResult = await _service.ListWebSite(new ListWebSiteInput
            {
                PageIndex = 100,
                PageSize = 10,
                IsHide = true
            });
            Assert.Empty(thanCountResult.Data.PageData);

            //var deletedListResult = await _service.ListWebSite(new ListWebSiteInput
            //{
            //    Key = WebSiteDatas.DedeletedWebSite.WebSiteName,
            //    IsHide = true
            //});

            //Assert.Empty(deletedListResult.Datas);
        }

        [Fact]
        public async Task GetWebSiteList_When_Found()
        {
            var noFilterResult = await _service.ListWebSite(new ListWebSiteInput
            {
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(noFilterResult.Data.PageData);
        }



        [Fact]
        public async Task SaveWebSite_When_Existed_WebSiteId_Or_WebSiteKey()
        {
            // Existed Id
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                var input = InputSample.GetRandomSaveWebSiteInput();
                input.Id = WebSiteDatas.SuperWebSite.Id;
                await _service.SaveWebSite(input);
            });
            // Existed WebSiteKey
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                var input = InputSample.GetRandomSaveWebSiteInput();
                input.WebSiteKey = WebSiteDatas.SuperWebSite.WebSiteKey;
                await _service.SaveWebSite(input);
            });
        }

        //[Fact]
        //public async Task SaveWebSite_When_Add_WebSiteMater()
        //{

        //}

        [Fact]
        public async Task SaveWebSite_When_Success()
        {
            var output = await _service.SaveWebSite(InputSample.NewSaveWebSiteInput);
            Assert.NotEmpty(output.Data.Safepassword);
        }

        [Fact]
        public async Task UpdateWebSite_When_Success()
        {
            var newInput = InputSample.GetRandomSaveWebSiteInput();
            await _service.SaveWebSite(newInput);
            var website = (await _service.GetWebSite(newInput.Id)).Data;
            //var updateInput = JObject.FromObject(website).ToObject<UpdateWebSiteInput>();
            var updateInput = website.MapTo<UpdateWebSiteInput>();
            updateInput.WebSiteChName = Guid.NewGuid().ToString();
            var result = await _service.UpdateWebSite(updateInput);
            Assert.Equal(1,result.Code);
        }

        [Fact]
        public async Task ResetPassword_When_Success()
        {
            var _=await _service.ResetPassword(new ResetPasswordInput
            {
                UserId = WebSiteDatas.SuperWebSite.WebSiteMater
            });
            Assert.Equal(1, _.Code);
        }

        
    }
}