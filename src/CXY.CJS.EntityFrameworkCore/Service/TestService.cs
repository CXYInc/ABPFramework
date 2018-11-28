using Abp.Application.Services;
using CXY.CJS.Models;
using CXY.CJS.Repository;

namespace CXY.CJS.Service
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
