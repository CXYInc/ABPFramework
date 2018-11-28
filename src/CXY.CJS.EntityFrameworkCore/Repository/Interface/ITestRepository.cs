using Abp.Domain.Repositories;
using CXY.CJS.Models;

namespace CXY.CJS.Repository
{
    public interface ITestRepository : IRepository<Test, string>
    {
        bool Add(Test entity);
    }
}
