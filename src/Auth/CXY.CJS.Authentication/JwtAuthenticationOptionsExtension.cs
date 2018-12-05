using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace CXY.CJS.JwtAuthentication
{
    public class JwtAuthenticationOptionsExtension
    {
        /// <summary>
        /// Token颁发机构
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 是否验证颁发机构
        /// </summary>
        public bool ValidateIssuer { get; set; }

        /// <summary>
        /// Token接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        ///  是否验证接收者
        /// </summary>
        public bool ValidateAudience { get; set; }

        /// <summary>
        /// Token有效分钟数
        /// </summary>
        public int ValidMinutes { get; set; }

        /// <summary>
        /// 签名密钥
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SecurityAlgorithms { get; set; }

        /// <summary>
        ///  Token验证前回调
        /// </summary>
        public Func<MessageReceivedContext, Task> OnMessageReceived { get; set; }

        /// <summary>
        /// Token验证失败回调
        /// </summary>
        public Func<AuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; }

        /// <summary>
        /// Token验证通过后回调
        /// </summary>
        public Func<TokenValidatedContext, Task> OnTokenValidated { get; set; }

        /// <summary>
        ///  未授权时调用
        /// </summary>
        public Func<JwtBearerChallengeContext, Task> OnChallenge { get; set; }

        public ISecurityTokenValidator SecurityTokenValidator { get; set; }
    }
}
