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
    public class ValuesController : CJSBaseController
    {
        private readonly JwtTokenProvider _jwtTokenProvider;
        private readonly IRepository<User, string> _repository;

        public ValuesController(JwtTokenProvider jwtTokenProvider, IHttpClientFactory httpClientFactory, IRepository<User, string> repository)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { AbpSession.UserId, AbpSession.UserName, AbpSession.WebSiteId };
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<string> GetToken(string id)
        {
            var user = await _repository.GetAsync(id);

            var claims = new List<Claim>
            {
                new Claim(ClaimConst.WebSiteId,user.WebSiteId),
                new Claim(ClaimConst.UserId,user.Id),
                new Claim(ClaimConst.UserName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, "CXY.CJS"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimTypes.Role,"Admin")
            };

            return _jwtTokenProvider.GenerateJwtToken(claims);
        }
    }
}
