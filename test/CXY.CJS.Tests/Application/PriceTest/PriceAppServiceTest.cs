using System.Threading.Tasks;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Tests.TestDatas;
using Xunit;

namespace CXY.CJS.Tests.Application.PriceTest
{
    public class PriceAppServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IPriceAppService _service;

        public PriceAppServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IPriceAppService>();
        }

        [Fact]
        public async Task QuotePrice_WhenSuccess()
        {
            var result = await _service.QuotePrice(new PriceInput
            {
                BatchId = BatchCarDatas.TestQuotePriceBatchCarDatas.BatchId
            });
            Assert.True(result.IsSuccess);
        }


    }

}