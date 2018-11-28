using System.Threading.Tasks;
using CXY.CJS.Web.Controllers;
using Shouldly;
using Xunit;

namespace CXY.CJS.Web.Tests.Controllers
{
    public class HomeController_Tests: CJSWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
