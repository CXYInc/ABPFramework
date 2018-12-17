using System;
using System.Linq;
using System.Net;
using Abp.Runtime.Validation;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CXY.CJS.Web.Core.Filter
{
    public class ApiErrorAttibute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //针对参数检查
            if (context.Exception is AbpValidationException v)
            {
                var erroMsgs = v.ValidationErrors.Select(e => e.ErrorMessage);
                context.Result = new JsonResult(new ApiResult().Error(erroMsgs.Aggregate((i, next) => $"{i},{next}")));
            }
            // 其他异常
            else if (context.Exception != null)
            {
                var env = GetHostingEnvironment(context);
                if (env.IsDevelopment())
                {
                    context.Result = new JsonResult(context.Exception);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (Int32)HttpStatusCode.BadRequest;
                    // todo:对输入和异常进行记录
                    // 此处应进行日志处理或特定值返回
                    context.Result = new JsonResult(new ApiResult().Error("无法处理请求"));
                }
            }
            base.OnException(context);
        }

        public IHostingEnvironment GetHostingEnvironment(ExceptionContext context)
        {
            return (IHostingEnvironment) context.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));
        }
    }
}