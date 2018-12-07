using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public interface ITestRepository : ICJSRepositoryBase<Test, string>
    {
        Test Add(Test entity);

        Test GetTest(string id);
    }
}
