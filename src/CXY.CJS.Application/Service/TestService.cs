using Abp.Application.Services;
using Abp.ObjectMapping;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CXY.CJS.Core.Enums;
using CXY.CJS.Core.Extension;
using System.Collections.Generic;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [Authorize]
    [Route("/api/services/app/[controller]")]
    public class TestService : CJSAppServiceBase, ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// 构造函数自动注入
        /// </summary>
        /// <param name="testRepository"></param>
        /// <param name="objectMapper"></param>
        public TestService(ITestRepository testRepository, IObjectMapper objectMapper)
        {
            _testRepository = testRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [AllowAnonymous]
        public Test Add(TestDtoInput entity)
        {
            var test = _objectMapper.Map<Test>(entity);
            return _testRepository.Add(test);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("Update")]
        [AllowAnonymous]
        public void Update()
        {
            _testRepository.Test();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Get/{id}")]
        [AllowAnonymous]
        public ApiResult<Test> GetTest(string id)
        {
            var result = new ApiResult<Test>
            {
                Code = 200,
            };

            result.Data = _testRepository.GetTest(id);

            return result;
        }

        /// <summary>
        /// 获取枚举
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Get/Enum")]
        [AllowAnonymous]
        public ApiResult GetEnumTest(int id)
        {
            var result = new ApiResult
            {
                Code = 200,
            };

            //result.Data = SortEnum.Desc.GetDescription();
            result.Data = id.ToEnum<SortEnum>().Item1.GetDescription();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("Get/TestEnum")]
        [AllowAnonymous]
        public List<TestOutDto> EnumMapperTest()
        {
          var list=  _testRepository.GetAll();

            return _objectMapper.Map<List<TestOutDto>>(list);
        }
    }
}
