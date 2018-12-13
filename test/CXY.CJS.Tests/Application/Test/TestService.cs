using CXY.CJS.Application;
using CXY.CJS.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CXY.CJS.Tests.Application.Test
{
    public class TestService : IClassFixture<CJSTestBase>
    {
        private readonly ITestService _testService;
        public TestService(CJSTestBase testBase)
        {
            _testService = testBase.Ioc.Resolve<ITestService>();
        }

        [Fact]
        public void Test()
        {
            _testService.Add(new TestDtoInput { UserId = Guid.NewGuid().ToString("N"), Name = "test" });
        }

        [Fact]
        public void Test1()
        {
            _testService.Update();
        }

    }
}
