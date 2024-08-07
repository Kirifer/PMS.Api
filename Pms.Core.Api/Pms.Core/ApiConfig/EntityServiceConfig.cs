using Microsoft.Extensions.DependencyInjection;
using Pms.Core.Extensions;

namespace Pms.Core.ApiConfig
{
    public static class EntityServiceConfig
    {
        public static IServiceCollection AddCoreEntityServices<TService>(
            this IServiceCollection services,
            string assemblyName)
        {
            services.RegisterAssemblies<TService>(assemblyName, DependencyLifetime.Scoped);
            return services;
        }
    }
}
