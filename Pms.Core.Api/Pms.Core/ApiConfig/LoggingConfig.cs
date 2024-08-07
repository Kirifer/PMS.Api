using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Pms.Core.ApiConfig
{
    public static class LoggingConfig
    {
        public static IServiceCollection AddCoreLogging(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddFilter("Microsoft", LogLevel.Warning);
                builder.AddFilter("System", LogLevel.Error);
                builder.AddFilter("Engine", LogLevel.Warning);
            });
            return services;
        }
    }
}
