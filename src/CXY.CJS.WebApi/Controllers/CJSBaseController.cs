using Abp.AspNetCore.Mvc.Controllers;
using CXY.CJS.Core.Extensions;

namespace CXY.CJS.WebApi.Controllers
{
    public class CJSBaseController : AbpController
    {
        //隐藏父类的AbpSession
        public new IAbpSessionExtension AbpSession { get; set; }
    }
}