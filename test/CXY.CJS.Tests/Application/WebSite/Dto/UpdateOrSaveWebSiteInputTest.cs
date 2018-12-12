using System.Threading.Tasks;
using CXY.CJS.Application;
using CXY.CJS.Repository.MixModel;
using Xunit;

namespace CXY.CJS.Tests.Application.WebSite
{
    public class UpdateOrSaveWebSiteInputTest : IClassFixture<CJSTestBase>
    {
        private readonly IWebSiteAppService _service;

        public UpdateOrSaveWebSiteInputTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IWebSiteAppService>();
        }

        [Fact]
        public async Task UpdateOrSaveWebSiteInputBase_Check()
        {
            var full = WebSiteFull.MapFrom(InputSample.NewSaveWebSiteInput);
            Assert.NotNull(full.WebSite);
        }
    }
}