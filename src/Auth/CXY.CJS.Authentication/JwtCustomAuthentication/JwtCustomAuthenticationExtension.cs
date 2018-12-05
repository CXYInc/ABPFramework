using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CXY.CJS.JwtAuthentication
{
    public static class JwtCustomAuthenticationExtension
    {
        public static IServiceCollection AddCustomAuthentication<TOptions, THandler>(this IServiceCollection services, Action<TOptions> configureOptions)
             where TOptions : AuthenticationSchemeOptions, new()
           where THandler : AuthenticationHandler<TOptions>
        {
            services.AddAuthentication()
                    .AddCustomAuthentication<TOptions, THandler>(configureOptions);
            return services;
        }

        public static AuthenticationBuilder AddCustomAuthentication<TOptions, THandler>(this AuthenticationBuilder builder, Action<TOptions> configureOptions) where TOptions : AuthenticationSchemeOptions, new()
           where THandler : AuthenticationHandler<TOptions>
        {
            return builder.AddCustomAuthentication<TOptions, THandler>("", configureOptions);
        }

        public static AuthenticationBuilder AddCustomAuthentication<TOptions, THandler>(this AuthenticationBuilder builder, string authenticationScheme, Action<TOptions> configureOptions) where TOptions : AuthenticationSchemeOptions, new()
          where THandler : AuthenticationHandler<TOptions>
        {
            return builder.AddScheme<TOptions, THandler>(authenticationScheme, configureOptions);
        }
    }
}
