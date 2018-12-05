using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.JwtAuthentication
{
    /// <summary>
    /// Jwt认证扩展
    /// </summary>
    public static class JwtAuthenticationExtension
    {
        private static JwtAuthenticationOptionsExtension _jwtOptionsExtension;

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, Action<JwtAuthenticationOptionsExtension> jwtOptionsExtension)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            _jwtOptionsExtension = new JwtAuthenticationOptionsExtension();
            jwtOptionsExtension(_jwtOptionsExtension);

            services.AddSingleton(new JwtTokenProvider(_jwtOptionsExtension));

            var authenticationBuilder = services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              });
            authenticationBuilder.AddJwtBearer(JwtBearerOptionAction);

            return services;
        }

        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseAuthentication();
        }

        private static void JwtBearerOptionAction(JwtBearerOptions jwtBearerOptions)
        {
            jwtBearerOptions.IncludeErrorDetails = true;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionsExtension.SigningKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                //是否验证签名
                ValidateIssuerSigningKey = true,
                //是否要求Token的Claims中必须包含Expires
                RequireExpirationTime = true,
                ValidIssuer = _jwtOptionsExtension.Issuer,
                ValidateIssuer = _jwtOptionsExtension.ValidateIssuer,
                ValidAudience = _jwtOptionsExtension.Audience,
                ValidateAudience = _jwtOptionsExtension.ValidateAudience,
                IssuerSigningKeys = new List<SecurityKey> { signingKey },
                //是否验证token有效期
                ValidateLifetime = true,
                //允许的服务器时间偏移量
                ClockSkew = TimeSpan.Zero
            };

            jwtBearerOptions.TokenValidationParameters = tokenValidationParameters;

            //自定义验证方式
            if (_jwtOptionsExtension.SecurityTokenValidator != null)
            {
                jwtBearerOptions.SecurityTokenValidators.Clear();
                jwtBearerOptions.SecurityTokenValidators.Add(_jwtOptionsExtension.SecurityTokenValidator);
            }

            jwtBearerOptions.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    if (_jwtOptionsExtension.OnMessageReceived == null)
                        return Task.CompletedTask;
                    return _jwtOptionsExtension.OnMessageReceived(context);
                },
                OnTokenValidated = context =>
                {
                    if (_jwtOptionsExtension.OnTokenValidated == null)
                        return Task.CompletedTask;
                    return _jwtOptionsExtension.OnTokenValidated(context);
                },
                OnAuthenticationFailed = context =>
                {
                    if (_jwtOptionsExtension.OnAuthenticationFailed == null)
                        return Task.CompletedTask;
                    return _jwtOptionsExtension.OnAuthenticationFailed(context);
                },
                OnChallenge = context =>
                {
                    if (_jwtOptionsExtension.OnChallenge == null)
                        return Task.CompletedTask;
                    return _jwtOptionsExtension.OnChallenge(context);
                }
            };
        }
    }
}
