using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Caching;
using Castle.Core.Logging;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Bus.Commands;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Repository;
using CXY.CJS.Tests.TestDatas;
using Moq;
using Xunit;

namespace CXY.CJS.Tests.Application.PriceTest
{
    public class IndoorPriceCommandHandlerTest : IClassFixture<CJSTestBase>
    {
        private readonly CJSTestBase _testBase;

        public IndoorPriceCommandHandlerTest(CJSTestBase testBase)
        {
            _testBase = testBase;
          
        }


        [Fact]
        public async Task IndoorPriceCommandHandler_When_Success()
        {
            IndoorPriceAndSaveCommand request =new IndoorPriceAndSaveCommand
            {
                GlobalKey = Guid.NewGuid().ToString(),
                IndoorPrice = new IndoorPriceInput
                {
                    BatchId = BatchInfoDatas.TestQuotePriceBatchInfo.Id,
                    CarNumber = BatchCarDatas.TestQuotePriceBatchCarDatas.CarNumber,
                    UserId = UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.Id
                }
            };

            var mockResult = new ApiResult<List<PriceResultOutput>>
            {
                Code = 1,
                Data = new List<PriceResultOutput>
                {
                    new PriceResultOutput
                    {
                        UserId= UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.Id,
                        ViolationType=1,
                        CanProcess = 1,
                        fcQuery=new List<FcQuery>
                        {
                            new FcQuery
                            {
                                ID = Guid.NewGuid().ToString(),
                                FC = 1.0m,
                                FCTYPE="下级分成",
                                WebSiteId =  UserSysSettingDatas.SuperWebSiteLowerAgentSysSetting.WebSiteId,
                                ProfitType=1,
                                ViolationType=1
                            }
                        }
                    }
                }
            };

            var mockPriceAppService = new Mock<IPriceAppService>();
            mockPriceAppService.Setup(i =>  i.IndoorPriceBatch(request.IndoorPrice))
                .Returns(Task.FromResult(mockResult));


            var service = new IndoorPriceCommandHandler(
                mockPriceAppService.Object,
                _testBase.Ioc.Resolve<ICacheManager>(),
                _testBase.Ioc.Resolve<ILogger>(),
                _testBase.Ioc.Resolve<IBatchAskPriceViolationAgentRepository>(),
                _testBase.Ioc.Resolve<IBatchCarRepository>(),
                _testBase.Ioc.Resolve<ICarViolationDivisionRepository>(),
                _testBase.Ioc.Resolve<IUserSysSettingRepository>());

            var testResult = await service.Handle(request,default(CancellationToken));
            Assert.Equal(mockResult.Data, testResult);
        }


    }

}