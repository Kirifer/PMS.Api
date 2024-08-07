using System.Diagnostics;

using Pms.Core.Database.Abstraction.Interface;
using Pms.Core.Database.Migration;

using Microsoft.Extensions.Logging;

namespace Pms.DataLayer
{
    public class PmsDbMigration : DbUpMigrationBase, IDbMigration
    {
        private readonly ILogger<PmsDbMigration> _logger;
        private readonly IDbConfig _dbSettings;

        public PmsDbMigration(
            ILogger<PmsDbMigration> logger,
            IDbConfig dbSettings)
            : base(logger)
        {
            _logger = logger;
            _dbSettings = dbSettings;
        }

        public void ExecuteMigration()
        {
            try
            {
                var connectionString = _dbSettings.ConnectionString;

                // Perform creation of database if not exist
                CreateDatabaseIfNotExist(connectionString);
                PerformScriptExecution(connectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError($"Migration failed: {ex.Message}");
            }
        }
    }
}
