using CXY.CJS.Application;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CXY.CJS.Web.Controllers
{
    public class HomeController : CJSControllerBase
    {
        private readonly ITestService _testService;

        public HomeController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Index()
        {
            _testService.Add(new Test { Id = Guid.NewGuid().ToString("N") });
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}