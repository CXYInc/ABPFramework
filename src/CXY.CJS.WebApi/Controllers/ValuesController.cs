using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CXY.CJS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : AbpController
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
