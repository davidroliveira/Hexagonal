using Dapper;
using Projeto.Domain.Connection;
using System.Data;
using System.Data.SqlClient;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class DbSession : IDbSession, IDisposable
{
    public long SessionId { private set; get; }
    public Guid ConnectionId { private set; get; }
    public IDbConnection? Connection { get; }
    public IDbTransaction? Transaction { get; set; }

    public DbSession(IDbSettings settings)
    {
        Connection = new SqlConnection(settings.ConnectionString);
        Connection.Open();
        DadosSessao();
    }

    private void DadosSessao() => (SessionId, ConnectionId) = Connection.QuerySingle<(long SessionId, Guid ConnectionId)>(
        @"select session_id SessionId, 
                     connection_id ConnectionId 
                from sys.dm_exec_connections 
               where session_id = @@spid");

    public void Dispose() => Connection?.Dispose();
}