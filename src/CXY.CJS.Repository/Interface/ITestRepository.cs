using Abp.Domain.Repositories;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface ITestRepository : ICJSRepositoryBase<Test, string>
    {
        bool Add(Test entity);
    }
}
