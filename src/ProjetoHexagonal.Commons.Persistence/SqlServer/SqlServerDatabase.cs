using System.Data;
using System.Data.SqlClient;

namespace ProjetoHexagonal.Commons.Persistence.SqlServer
{
    public sealed class SqlServerDatabase : IDatabase
    {
        private readonly SqlConnectionStringBuilder connectionString;

        public SqlServerDatabase(string connectionString)
        {
            this.connectionString = new SqlConnectionStringBuilder(connectionString);
        }

        public string ConnectionString => connectionString.ToString();

        public string Name => connectionString.InitialCatalog;

        public IDbConnection Connect()
        {
            var connection = new SqlConnection(connectionString.ToString());

            connection.Open();

            return connection;
        }

        public IConnection Connection()
        {
            var connection = new SqlConnection(connectionString.ToString());

            connection.Open();

            return new SqlServerConnection(connection);
        }
    }
}
