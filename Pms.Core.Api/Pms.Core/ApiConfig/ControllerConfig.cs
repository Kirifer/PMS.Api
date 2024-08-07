using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

using Pms.Shared;
using Pms.Shared.Enums;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Pms.Core.Api
{
    public static class ControllerConfig
    {
        public static IServiceCollection AddCoreControllers(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        var jsonOptions = options.JsonSerializerOptions;
                        jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    }).ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = c =>
                        {
                            var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                                .SelectMany(v => v.Errors)
                                .Select(v => new ErrorDto(ErrorCode.ValidationError, v.ErrorMessage)).ToList();

                            return new BadRequestObjectResult(new
                            {
                                Errors = errors,
                                Code = HttpStatusCode.BadRequest,
                                Succeeded = false
                            });
                        };
                    });
            return services;
        }
    }
}
