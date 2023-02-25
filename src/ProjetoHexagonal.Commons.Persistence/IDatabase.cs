using System.Data;

namespace ProjetoHexagonal.Commons.Persistence
{
    public interface IDatabase
    {
        string ConnectionString { get; }

        IDbConnection Connect();

        IConnection Connection();
    }
}
