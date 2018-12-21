using Abp.Application.Services;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestService : IApplicationService
    {
        Test Add(TestDtoInput entity);

        void Update();

        ApiResult<Test> GetTest(string id);

        List<TestOutDto> EnumMapperTest();

        void SendEmailTest();

        /// <summary>
        ///  获取文件
        /// </summary>
        /// <returns>item1:文件流 item2:文件名 item3:文件类型</returns>
        Tuple<Stream, string, string> CreateExcel();
    }
}
