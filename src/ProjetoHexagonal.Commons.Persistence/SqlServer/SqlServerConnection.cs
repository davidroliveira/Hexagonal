using System.Data.SqlClient;

namespace ProjetoHexagonal.Commons.Persistence.SqlServer
{
    public sealed class SqlServerConnection : IConnection
    {
        private readonly SqlConnection connection;

        public SqlServerConnection(SqlConnection connection)
        {
            this.connection = connection;
        }

        public ICommand Command(string text)
        {
            var command = connection.CreateCommand();
            command.CommandText = text;
            return new SqlServerCommand(command);
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
