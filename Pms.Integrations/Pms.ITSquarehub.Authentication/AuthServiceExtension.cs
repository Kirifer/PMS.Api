using Microsoft.Extensions.DependencyInjection;

using Pms.ITSquarehub.Authentication.Service;

namespace Pms.ITSquarehub.Authentication
{
    public static class AuthServiceExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<IITSAuthService, AuthenticationService>();

            return services;
        }
    }
}
