using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace CXY.CJS.WebApi
{
    public class JwtCustomValidator : ISecurityTokenValidator
    {
        private JwtSecurityTokenHandler _tokenHandler;

        public JwtCustomValidator()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        bool ISecurityTokenValidator.CanValidateToken => true;

        int ISecurityTokenValidator.MaximumTokenSizeInBytes { get; set; }

        bool ISecurityTokenValidator.CanReadToken(string securityToken)
        {
            return true;
        }

        //验证token
        ClaimsPrincipal ISecurityTokenValidator.ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            try
            {
                //解析token
                var tokenS = _tokenHandler.ReadToken(securityToken) as JwtSecurityToken;
                //获取token里的值
                var jti = tokenS.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value;

                //自定义验证

                //验证是否有效
                var principal = _tokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
                return principal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
