using Abp.Application.Services;
using CXY.CJS.Models;

namespace CXY.CJS.Service
{
    public interface ITestService : IApplicationService
    {
        bool Add(Test entity);
    }
}
