using DbUp;
using DbUp.Helpers;
using Microsoft.Extensions.Logging;

namespace Pms.Core.Database.Migration
{
    public abstract class DbUpMigrationBase
    {
        private readonly ILogger<DbUpMigrationBase> _logger;

        public DbUpMigrationBase(ILogger<DbUpMigrationBase> logger)
        {
            _logger = logger;
        }

        protected void CreateDatabaseIfNotExist(string connectionString)
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);
        }

        protected void PerformScriptExecution(string connectionString)
        {
            _logger.LogInformation("Performing upgrade for all scripts.");

            var upgradeEngineBuilder = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(GetType().Assembly, f => !f.Contains(".AlwaysRun."))
                .WithTransaction()
                .LogToConsole()
                .Build();

            var result = upgradeEngineBuilder.PerformUpgrade();
            if (result.Successful)
            {
                var alwaysRunUpgrader = DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsAndCodeEmbeddedInAssembly(GetType().Assembly, f => f.Contains(".AlwaysRun."))
                    .JournalTo(new NullJournal())
                    .WithTransaction()
                    .LogToConsole()
                    .Build();

                alwaysRunUpgrader.PerformUpgrade();
            }

            _logger.LogInformation("Upgrade ended successfully.");
        }
    }
}
