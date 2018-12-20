using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Castle.Core.Logging;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Utils;
using CXY.CJS.Core.WebApi;
using CXY.CJS.JwtAuthentication;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CXY.CJS.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AuthController : CJSBaseController
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly IRepository<Users, string> _repository;
        private readonly JwtBearerConfig _jwtBearerConfig;
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jwtTokenProvider"></param>
        /// <param name="repository"></param>
        public AuthController(JwtBearerConfig jwtBearerConfig, JwtTokenProvider jwtTokenProvider, IRepository<Users, string> repository, ILogger logger)
        {
            _jwtBearerConfig = jwtBearerConfig;
            _jwtTokenProvider = jwtTokenProvider;
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// 获取用户登录信息接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> GetUserInfo()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<LoginResult>> UserLogin([FromBody] UserLoginInfo loginInfo)
        {
            var result = new ApiResult<LoginResult>().Success();

            _logger.Info("test");

            if (loginInfo == null)
            {
                return result.Error("无效的登录信息");
            }

            try
            {
                var user = await _repository.FirstOrDefaultAsync(x => x.UserName == loginInfo.LoginName && x.WebSiteId == loginInfo.WebSiteId);

                if (user == null)
                {
                    return result.Error("账号不存在");
                }

                if (user.Password != Encryptor.MD5Entry(loginInfo.Password))
                {
                    return result.Error("账号或密码错误");
                }

                var claims = new List<Claim>
                {
                new Claim(ClaimConst.WebSiteId,user.WebSiteId),
                new Claim(ClaimConst.UserId,user.Id),
                new Claim(ClaimConst.UserName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, "CXY.CJS"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N"))
               };

                var token = _jwtTokenProvider.GenerateJwtToken(claims);

                result.Data = new LoginResult { Token = token, Expired = _jwtBearerConfig.ValidMinutes * 60 };
            }
            catch (EntityNotFoundException)
            {
                result.Code = 0;
                result.Message = "账号或密码错误";
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;
        }
    }
}
