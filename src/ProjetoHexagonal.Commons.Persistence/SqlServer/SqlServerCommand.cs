using System.Data.SqlClient;

namespace ProjetoHexagonal.Commons.Persistence.SqlServer
{
    public sealed class SqlServerCommand : ICommand
    {
        private readonly SqlCommand command;

        public SqlServerCommand(SqlCommand command)
        {
            this.command = command;
        }

        public int Timeout
        {
            get => command.CommandTimeout;
            set => command.CommandTimeout = value;
        }

        public ICommand AddParameter(string name, object? value)
        {
            value ??= DBNull.Value;

            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            command.Parameters.Add(parameter);

            return this;
        }

        public void ExecuteNonQuery(int minimumNumberOfRowsAffected = 1)
        {
            var numberOfRowsAffected = command.ExecuteNonQuery();

            if (numberOfRowsAffected < minimumNumberOfRowsAffected)
            {
                throw new InvalidOperationException($"O número de linhas afetadas '{numberOfRowsAffected}' é diferente do mínimo esperado '{minimumNumberOfRowsAffected}'.");
            }
        }

        public IReader ExecuteReader()
        {
            return new SqlServerReader(command.ExecuteReader());
        }

        public TScalar ExecuteScalar<TScalar>()
        {
            return (TScalar)command.ExecuteScalar();
        }

        public void Dispose()
        {
            command.Dispose();
        }
    }
}
