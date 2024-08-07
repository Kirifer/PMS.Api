namespace Pms.Core.Config.Database
{
    public class DatabaseConfig
    {
        public required string Host { get; set; }
        public required string Port { get; set; }
        public required string DatabaseName { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }
}
