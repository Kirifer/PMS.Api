using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Pms.Core.ApiConfig
{
    public static class CompressionConfig
    {
        public static IServiceCollection AddCoreCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    "text/plain",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                };
            });

            return services;
        }
    }
}
