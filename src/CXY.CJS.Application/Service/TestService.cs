using Abp.Application.Services;
using Abp.Json;
using CXY.CJS.HttpClient;
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
        private readonly HttpClientHelper _httpClientHelper;
        /// <summary>
        /// 构造函数自动注入
        /// </summary>
        /// <param name="testRepository"></param>
        public TestService(ITestRepository testRepository, HttpClientHelper httpClientHelper)
        {
            _testRepository = testRepository;
            _httpClientHelper = httpClientHelper;
        }

        [HttpPost("Create")]
        public Test Add(Test entity)
        {
            return _testRepository.Add(entity);
        }

        [HttpPost("Get/{id}")]
        [AllowAnonymous]
        public Test GetTest(string id)
        {
           var t= _httpClientHelper.GetAsync<string>(null).Result;
            return _testRepository.GetTest(id);
        }
    }
}
