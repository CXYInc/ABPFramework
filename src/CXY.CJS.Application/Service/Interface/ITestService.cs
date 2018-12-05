using Abp.Application.Services;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.Application
{
    public interface ITestService : IApplicationService
    {
        [HttpPost]
        Test Add(Test entity);

      
        Test GetTest(string id);
    }
}
