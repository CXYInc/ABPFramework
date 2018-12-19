using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.Auditing;
using Abp.Collections.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Validation;
using Abp.UI;
using Castle.Core.Logging;
using CXY.CJS.Application;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CXY.CJS.Web.Core.Filter
{
    public class ApiErrorAttibute : ExceptionFilterAttribute,IActionFilter
    {
        private ILogger _logger;
        private IDictionary<string, object> _arguments;

        public override  void OnException(ExceptionContext context)
        {

            var env = GetService<IHostingEnvironment>(context);
            _logger= GetService<ILogger>(context);
            var info = new
            {
                ServiceName = context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType()
                              != null ? context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType().FullName : "",
                MethodName = context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo.Name,
                Parameters = ConvertArgumentsToJson(_arguments)
            };

            switch (context.Exception)
            {
                //针对参数检查
                case AbpValidationException v:
                    ValidationExHandler(context, v);
                    break;
                case UserFriendlyException u:

                    _logger.ErrorFormat("Service:{0}.{1};Parameters:{2};ExecutionTime:{3};Exception:{4}",
                         info.ServiceName,info.MethodName,info.Parameters,DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"), context.Exception.Message);
                    context.Result = new JsonResult(context.Exception.Message);
                    break;
                default:
                    if (env.IsDevelopment())
                    {
                        context.Result = new JsonResult(context.Exception);
                    }
                    else
                    {
                        _logger.ErrorFormat("Service:{0}.{1};Parameters:{2};ExecutionTime:{3};Exception:{4}",
                            info.ServiceName, info.MethodName, info.Parameters, DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"), context.Exception);
                        context.HttpContext.Response.StatusCode = (Int32)HttpStatusCode.BadRequest;
                        // 此处应进行日志处理或特定值返回
                        context.Result = new JsonResult(new ApiResult().Error("无法处理请求"));
                    }
                    break;
            }
            base.OnException(context);
        }


        private static void ValidationExHandler(ExceptionContext context, AbpValidationException v)
        {
            var erroMsgs = v.ValidationErrors.Select(e => e.ErrorMessage);
            context.Result = new JsonResult(new ApiResult().Error(erroMsgs.Aggregate((i, next) => $"{i},{next}")));
        }


        private static TService GetService<TService>(ExceptionContext context)
        {
            return (TService)context.HttpContext.RequestServices.GetService(typeof(TService));
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableRewind();
            using (var reader = new StreamReader(context.Request.Body))
            {
                var body = reader.ReadToEnd();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                body = await reader.ReadToEndAsync();
                return body;
            }
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }
                return JsonConvert.SerializeObject(arguments);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _arguments = context.ActionArguments;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}