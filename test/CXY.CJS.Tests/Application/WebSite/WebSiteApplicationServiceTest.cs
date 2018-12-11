using System;
using System.Threading.Tasks;
using CXY.CJS.Application;
using CXY.CJS.Role;
using CXY.CJS.Role.Dto;
using Xunit;

namespace CXY.CJS.Tests.Application.WebSite
{
    public class WebSiteApplicationServiceTest : CJSTestBase
    {
        private readonly IWebSiteAppService _service;

        public WebSiteApplicationServiceTest()
        {
            _service = Resolve<IWebSiteAppService>();
        }

        [Fact]
        public async Task GetWebSite_Must_NotFund()
        {
            var result = await _service.Get(Guid.NewGuid().ToString()+"1234");
            Assert.Null(result);


        }
    }
}