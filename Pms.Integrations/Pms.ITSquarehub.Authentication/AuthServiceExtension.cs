using Microsoft.Extensions.DependencyInjection;

using Pms.Core.Extensions;
using Pms.ITSquarehub.Authentication.Config;
using Pms.ITSquarehub.Authentication.Service;

namespace Pms.ITSquarehub.Authentication
{
    public static class AuthServiceExtension
    {
        public static IServiceCollection AddITSAuth(this IServiceCollection services, Func<AuthenticationConfig> configFunc)
        {
            services.AddScoped<IAuthenticationConfig, AuthenticationConfig>(service => configFunc());
            services.AddScoped<IITSAuthService, ITSAuthService>();
            services.RegisterHttpClientServices<IITSAuthService>("Pms.ITSquarehub.Authentication");

            return services;
        }
    }
}
