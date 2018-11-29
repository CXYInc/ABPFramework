using Abp.Application.Services;
using CXY.CJS.Models;

namespace CXY.CJS.Service
{
    interface ITestService : IApplicationService
    {
        bool Add(Test entity);
    }
}
