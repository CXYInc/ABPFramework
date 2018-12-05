using Abp.AspNetCore.Mvc.Controllers;
using CXY.CJS.JwtAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CXY.CJS.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ValuesController : AbpController
    {
        private readonly JwtTokenProvider _jwtTokenProvider;

        public ValuesController(JwtTokenProvider jwtTokenProvider)
        {
            _jwtTokenProvider = jwtTokenProvider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public string GetToken(int id)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "hausthy"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimTypes.Role,"Admin")
            };

            return _jwtTokenProvider.GenerateJwtToken(claims);
        }
    }
}
