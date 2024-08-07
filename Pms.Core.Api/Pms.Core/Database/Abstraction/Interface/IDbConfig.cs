namespace Pms.Core.Database.Abstraction.Interface
{
    public interface IDbConfig
    {
        string Host { get; set; }

        uint Port { get; set; }

        string Database { get; set; }

        string User { get; set; }

        string Password { get; set; }

        bool Pooling { get; }

        string ConnectionString { get; }
    }
}
