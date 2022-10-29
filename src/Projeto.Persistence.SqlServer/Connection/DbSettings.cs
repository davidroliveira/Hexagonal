using Projeto.Domain.Connection;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class DbSettings : IDbSettings
{
    public string ConnectionString => "Data Source=desktop-6FB4NG2;Initial Catalog=Projeto;User ID=sa;Password=123";
}