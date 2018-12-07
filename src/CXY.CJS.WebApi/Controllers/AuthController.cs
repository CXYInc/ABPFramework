using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using CXY.CJS.Constant;
using CXY.CJS.JwtAuthentication;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CXY.CJS.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AuthController : CJSBaseController
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly IRepository<User, string> _repository;

        public AuthController(JwtTokenProvider jwtTokenProvider, IHttpClientFactory httpClientFactory, IRepository<User, string> repository)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> GetUserInfo()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> UserLogin([FromBody] UserLoginInfo loginInfo)
        {
            var result = new ApiResult<string>().Success();

            if (loginInfo == null)
            {
                return result.Error("无效的登录信息");
            }

            try
            {
                var user = await _repository.SingleAsync(x => x.UserName == loginInfo.LoginName && x.Password == loginInfo.Password && x.WebSiteId == loginInfo.WebSiteId);

                if (user == null)
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

                result.Data = token;
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
