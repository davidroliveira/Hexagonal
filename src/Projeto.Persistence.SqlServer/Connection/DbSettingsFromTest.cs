using Projeto.Domain.Connection;
using System.Data.SqlClient;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class DbSettingsFromTest : IDbSettings
{
    public string ConnectionString { get; }

    public DbSettingsFromTest()
    {
        var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(new DbSettings().ConnectionString);
        //todo: Ajustar Banco de Dados de Testes : sqlConnectionStringBuilder.InitialCatalog += "-Test";
        ConnectionString = sqlConnectionStringBuilder.ToString();
    }
}