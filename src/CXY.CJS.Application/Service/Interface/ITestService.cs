using Abp.Application.Services;
using CXY.CJS.Model;

namespace CXY.CJS.Application
{
    public interface ITestService : IApplicationService
    {
        bool Add(Test entity);
    }
}
