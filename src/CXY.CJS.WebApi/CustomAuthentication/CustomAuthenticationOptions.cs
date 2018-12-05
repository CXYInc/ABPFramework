using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CXY.CJS.WebApi
{
    public class CustomAuthenticationOptions : AuthenticationSchemeOptions
    {
        public ClaimsIdentity Identity { get; set; }
    }
}
