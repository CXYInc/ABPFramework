using CXY.CJS.Constant;
using CXY.CJS.JwtAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace CXY.CJS.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ValuesController : CJSBaseController
    {
        private readonly JwtTokenProvider _jwtTokenProvider;

        public ValuesController(JwtTokenProvider jwtTokenProvider, IHttpClientFactory httpClientFactory)
        {
            _jwtTokenProvider = jwtTokenProvider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public string GetToken(int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimConst.WebSiteId,"009020"),
                new Claim(ClaimConst.UserId,"009020123456"),
                new Claim(ClaimConst.UserName,"chelutong"),
                new Claim(JwtRegisteredClaimNames.Sub, "hausthy"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimTypes.Role,"Admin")
            };

            return _jwtTokenProvider.GenerateJwtToken(claims);
        }
    }
}
