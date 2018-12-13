using Abp.Application.Services;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.Application
{
    public interface ITestService : IApplicationService
    {
        Test Add(TestDtoInput entity);

        void Update();

        ApiResult<Test> GetTest(string id);
    }
}
