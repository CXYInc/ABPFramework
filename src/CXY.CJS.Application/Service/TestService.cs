using Abp.ObjectMapping;
using Castle.Core.Logging;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Application.Service.Dtos;
using CXY.CJS.Core.Enums;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.NPOI;
using CXY.CJS.Core.Utils.Mail;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 测试服务
    /// </summary>
    //[Authorize]
    [Route("/api/services/app/[controller]")]
    [Authorize]
    public class TestService : CJSAppServiceBase, ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly ISystemSmtpSender _emailSender;
        private readonly ILogger _logger;
        /// <summary>
        /// 构造函数自动注入
        /// </summary>
        /// <param name="testRepository"></param>
        /// <param name="objectMapper"></param>
        public TestService(ITestRepository testRepository, IObjectMapper objectMapper, ISystemSmtpSender emailSender, ILogger logger)
        {
            _testRepository = testRepository;
            _objectMapper = objectMapper;
            _emailSender = emailSender;
            _logger = logger;
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
            var list = _testRepository.GetAll();

            return _objectMapper.Map<List<TestOutDto>>(list);
        }

        [HttpPost("SendEmailTest")]
        [Authorize]
        public void SendEmailTest()
        {
            _emailSender.Send("someone@qq.com", "测试邮件，请勿回复", "收到请勿回复!");
        }


        [HttpPost("LoggerInfo")]
        [AllowAnonymous]
        public void LoggerInfo()
        {
            var exampleUser = new Users { Id = "1", RealName = "Adam", CreationTime = DateTime.Now };
            _logger.InfoFormat("Created {@User} on {Created}", exampleUser, DateTime.Now);
            _logger.Debug("Debug");
            _logger.Warn("Warn");
            _logger.Error("Error");
        }

        [HttpPost("UploadExcel")]
        [AllowAnonymous]
        public IList<BatchTableModelDto> UploadExcel([FromForm] TestFile testFile)
        {
            var ds = NPOIExcelHelper.ReadExcel(testFile.File);

            var tempTable = ds.Tables["订单信息"];

            var list = tempTable.ConvertToModel<BatchTableModelDto>();

            var dt = list.ListToDataTable();

            return list;
        }

        /// <summary>
        ///  获取文件
        /// </summary>
        /// <returns>item1:文件流 item2:文件名 item3:文件类型</returns>
        [NonAction]
        public Tuple<Stream, string, string> CreateExcel()
        {
            var addrUrl = NPOIExcelHelper.ExportTest();

            string fileExt = Path.GetExtension(addrUrl);

            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var contentType = provider.Mappings[fileExt];

            addrUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, addrUrl);

            var stream = new FileStream(addrUrl, FileMode.Open);

            return new Tuple<Stream, string, string>(stream, Path.GetFileName(addrUrl), contentType);
        }
    }
}
