using ProjetoHexagonal.Commons.Persistence.SqlServer;
using ProjetoHexagonal.Domain.Repositories;

namespace ProjetoHexagonal.Persistence.Repositories;

public sealed class ModeloRepository : IModeloRepository
{
    private readonly SqlServerDatabase database;

    public ModeloRepository(string connectionString) => database = new SqlServerDatabase(connectionString);

    public string ConsultaMensagemModelo(long codigoModelo)
    {
        using var connection = database.Connection();
        using var command = connection.Command($@"
             SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
          SELECT NOME
            FROM MODELOS
           WHERE COD_MODELO = @COD_MODELO");

        var result = command
            .AddParameter("COD_MODELO", codigoModelo)
            .ExecuteScalar<string>();

        return result;
    }
}