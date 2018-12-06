using Abp.Application.Services;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.Application
{
    [Authorize]
    [Route("/api/services/app/[controller]")]
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

        [HttpPost("Create")]
        public Test Add(Test entity)
        {
            return _testRepository.Add(entity);
        }

        [HttpPost("Get/{id}")]
        public Test GetTest(string id)
        {
            return _testRepository.GetTest(id);
        }
    }
}
