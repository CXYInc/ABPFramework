using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Tests.TestDatas;
using Xunit;

namespace CXY.CJS.Tests.Application.BatchInfoTest
{
    public class BatchInfoAppServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IBatchInfoAppService _service;
        public BatchInfoAppServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IBatchInfoAppService>();
        }

        [Fact]
        public async Task GetBatchInfosList_When_Found()
        {
            var noCompleteResult = await _service.ListBatchInfo(new GetBatchInfosListInput
            {
                //WebSiteId = BatchInfoDatas.NoCompleteBatchInfo.WebSiteId,
                Id = BatchInfoDatas.NoCompleteBatchInfo.Id.Remove(1),
                Status = new List<int> { BatchInfoDatas.NoCompleteBatchInfo.Status },
                Proxy = BatchInfoDatas.NoCompleteBatchInfo.Proxy,
                TimeEnum = BatchInfosListInputTimeEnum.CreationTime,
                StartTime = DateTime.Now.AddMonths(-1),
                EndTimeTime = DateTime.Now,
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(noCompleteResult.Data.PageData);


            var completeResult = await _service.ListBatchInfo(new GetBatchInfosListInput
            {
                //WebSiteId = BatchInfoDatas.CompleteBatchInfo.WebSiteId,
                Id = BatchInfoDatas.CompleteBatchInfo.Id.Remove(1),
                Status = new List<int> { BatchInfoDatas.CompleteBatchInfo.Status },
                Proxy = BatchInfoDatas.CompleteBatchInfo.Proxy,
                TimeEnum = BatchInfosListInputTimeEnum.CompleteTime,
                StartTime = DateTime.Now.AddMonths(-1),
                EndTimeTime = DateTime.Now,
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(completeResult.Data.PageData);
        }

        [Fact]
        public async Task DeleteBatchInfo_When_Found()
        {
            await _service.Delete(BatchInfoDatas.WillBeDelBatchInfo.Id);
            var datas = await _service.ListBatchInfo(new GetBatchInfosListInput
            {
                Id = BatchInfoDatas.WillBeDelBatchInfo.Id
            });
            Assert.Null(datas.Data.PageData.FirstOrDefault(i => i.Id == BatchInfoDatas.WillBeDelBatchInfo.Id));
        }


        public static readonly IEnumerable<object[]> GetBatchNoInput = new List<object[]>
        {
            new object[]{null},
            new object[]{DateTime.Now}
        };



        [Theory]
        [MemberData(nameof(GetBatchNoInput))]
        public async Task GetBatchNo_When_Success(DateTime? time = null)
        {
            var result = await _service.GetBatchNo(time);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task SaveBatchInfo_When_Success()
        {
            var result = await _service.SaveBatchInfo(new SaveBatchInfoInput
            {
                Id = Guid.NewGuid().ToString(),
                Proxy = BatchInfoDatas.NoCompleteBatchInfo.Proxy,
                ProxyUserId = BatchInfoDatas.NoCompleteBatchInfo.ProxyUserId,
                Remark = ""
            });
            Assert.Equal(1, result.Code);
        }


        public static  IEnumerable<object[]> ForcedCompletedInput = new List<object[]>
        {
            //new object[]{ Guid.NewGuid().ToString(), 0},
            new object[]{ BatchInfoDatas.ProcessingBatchInfo.Id,1},
            //new object[]{ BatchInfoDatas.NoCompleteBatchInfo.Id, 0},
            //new object[]{ BatchInfoDatas.CompleteBatchInfo.Id,0},
        };


        [Fact]
        public async Task ForcedCompleted_OnlyProcessing_Success()
        {
            var noExsitedReuslt = await _service.ForcedCompleted(Guid.NewGuid().ToString());
            Assert.Equal(0, noExsitedReuslt.Code);

            var notAllowResult = await _service.ForcedCompleted(BatchInfoDatas.NoCompleteBatchInfo.Id);
            Assert.Equal(0, notAllowResult.Code);

            var onlySuccessResult = await _service.ForcedCompleted(BatchInfoDatas.ProcessingBatchInfo.Id);
            Assert.Equal(1, onlySuccessResult.Code);
        }




    }
}