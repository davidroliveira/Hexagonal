using Projeto.Domain.Connection;
using System.Data;
using System.Data.SqlClient;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class DbSession : IDbSession
{
    public IDbConnection Connection { get; }

    public IDbTransaction Transaction { get; set; } = null!;

    public DbSession(IDbSettings settings)
    {
        Connection = new SqlConnection(settings.ConnectionString);
        Connection.Open();
    }

    public void Dispose() => Connection.Dispose();
}