using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CXY.CJS.JwtAuthentication
{
    public class JwtTokenProvider
    {
        private readonly JwtAuthenticationOptionsExtension _jwtOptionsExtension;
        public JwtTokenProvider(JwtAuthenticationOptionsExtension jwtOptionsExtension)
        {
            _jwtOptionsExtension = jwtOptionsExtension;
        }

        public string GenerateJwtToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionsExtension.SigningKey));
            var creds = new SigningCredentials(key, _jwtOptionsExtension.SecurityAlgorithms);
            var addClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp,DateTimeOffset.Now.AddMinutes(_jwtOptionsExtension.ValidMinutes).ToUnixTimeSeconds().ToString())
                };
            if (claims == null || !claims.Any())
                claims = new List<Claim>();

            claims.AddRange(addClaims);
            var token = new JwtSecurityToken(_jwtOptionsExtension.Issuer, _jwtOptionsExtension.Audience, claims, signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
