using Abp.Application.Services;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using System.Collections.Generic;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestService : IApplicationService
    {
        Test Add(TestDtoInput entity);

        void Update();

        ApiResult<Test> GetTest(string id);

        List<TestOutDto> EnumMapperTest();

        void SendEmailTest();
    }
}
