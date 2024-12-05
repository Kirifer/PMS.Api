using Pms.Core.Authentication;
using Pms.Core.Database;
using Pms.Core.Database.Abstraction;
using Pms.Core.Database.Abstraction.Interface;
using Pms.Core.Extensions;
using Pms.Datalayer;
using Pms.Datalayer.Implementation;
using Pms.Datalayer.Interface;
using Pms.DataLayer;
using Pms.Domain.Services.Config;

namespace Pms.Api.Configurations
{
    public static class PmsDatabaseConfig
    {
        public static IServiceCollection AddPmsDatabase(this IServiceCollection services, IMicroServiceConfig envConfig)
        {
            services.AddSingleton<IDbConfig, PmsDbConfig>(
                config => new PmsDbConfig()
                {
                    Host = "localhost",
                    Port = 5432,
                    Database = "itsquarehub-pms",
                    User = "postgres",
                    Password = "jerald123",
                    Pooling = true
                }
            );

            services.AddTransient<IDbMigration, PmsDbMigration>();
            services.AddTransient<IDbUserContext>(provider =>
            {
                var userContext = provider.GetService<IUserContext>();
                return new DbUserContext(userContext!);
            });
            services.AddTransient(provider =>
            {
                var dbConfig = provider.GetRequiredService<IDbConfig>();
                var dbUserContext = provider.GetRequiredService<IDbUserContext>();
                var dbMigration = provider.GetRequiredService<IDbMigration>();
                return new PmsDbContext(dbConfig, dbUserContext, dbMigration);
            });

            services.AddScoped<IUserRepository, UserRepository>();


            services.RegisterAssemblies<IDbQuerySingle>("Pms.DataLayer", DependencyLifetime.Transient);
            services.RegisterAssemblies<IDbQuery>("Pms.DataLayer", DependencyLifetime.Transient);
            services.RegisterAssemblies<IDbCommand>("Pms.DataLayer", DependencyLifetime.Transient);

            return services;
        }

        public static IApplicationBuilder UseAtsDatabase(this IApplicationBuilder app)
        {
            var migrationRef = app.ApplicationServices.GetService<IDbMigration>();
            migrationRef!.ExecuteMigration();

            return app;
        }
    }
}
