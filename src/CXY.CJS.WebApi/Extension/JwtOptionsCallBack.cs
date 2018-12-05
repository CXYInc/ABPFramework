using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace CXY.CJS.WebApi
{
    public class JwtOptionsCallBack
    {
        /// <summary>
        /// 接收到消息后回调
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task OnMessageReceived(MessageReceivedContext context)
        {
            var token = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(token) && token.ToString().StartsWith(JwtBearerDefaults.AuthenticationScheme))
            {
                var tokens = token.ToString().Split(" ");
                if (tokens.Length == 2)
                {
                    token = tokens[1];
                }
                else
                {
                    token = "";
                }
            }
            else
            {
                token = "";
            }
            context.Token = token;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 认证失败时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = context.Response.StatusCode, Data = false, Message = "无效的Token信息" }));
                }
                return Task.CompletedTask;
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// Token验证通过后调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task OnTokenValidated(TokenValidatedContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = context.Response.StatusCode, Data = false, Message = "无法访问的资源" }));
                }
                return Task.CompletedTask;
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 未授权时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task OnChallenge(JwtBearerChallengeContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = context.Response.StatusCode, Data = false, Message = "无法访问的资源" }));
                }
                return Task.CompletedTask;
            });
            return Task.CompletedTask;
        }
    }
}
