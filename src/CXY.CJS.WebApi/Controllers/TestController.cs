using CXY.CJS.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.WebApi.Controllers
{
    [Route("/api/services/app/[controller]")]
    public class TestController : CJSBaseController
    {
        private readonly ITestService _testService;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("CreateExcel")]
        [AllowAnonymous]
        public ActionResult CreateExcel()
        {
            var tupl = _testService.CreateExcel();

            return File(tupl.Item1, tupl.Item3, tupl.Item2);
        }
    }
}
