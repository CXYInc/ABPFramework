using Abp.Application.Services;
using CXY.CJS.Model;
using CXY.CJS.Repository;

namespace CXY.CJS.Application
{
    public class TestService : ApplicationService, ITestService
    {
        private readonly ITestRepository _testRepository;

        /// <summary>
        /// 构造函数自动注入
        /// </summary>
        /// <param name="testRepository"></param>
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public bool Add(Test entity)
        {
            return _testRepository.Add(entity);
        }
    }
}
