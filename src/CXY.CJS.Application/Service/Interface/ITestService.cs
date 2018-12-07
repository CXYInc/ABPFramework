using Abp.Application.Services;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using CXY.CJS.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.Application
{
    public interface ITestService : IApplicationService
    {
        [HttpPost]
        Test Add(TestDtoInput entity);

        ApiResult<Test> GetTest(string id);
    }
}
