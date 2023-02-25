using ProjetoHexagonal.Commons.Persistence.SqlServer;
using System.Data.SqlClient;

namespace ProjetoHexagonal.Persistence.TestSupports;

public sealed class DatabaseFixture
{
    public static DatabaseFixture FromEnvironmentOrDefault()
    {
        var connectionString = new SqlConnectionStringBuilder
        {
            DataSource = "localhost,1433",
            UserID = "sa",
            Password = "KeDViAc6kZDd1y17"
        }.ToString();

        return new DatabaseFixture(connectionString);
    }

    private DatabaseFixture(string connectionString)
    {
        Database = new SqlServerDatabase(new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = $"PROJETO_HEXAGONAL-{Guid.NewGuid():N}",
        }.ToString());
    }

    public SqlServerDatabase Database { get; }

    public void SetUp()
    {
        CreateDatabase();
        MigrateDatabase();
    }

    public void TearDown()
    {
        DestroyDatabase();
    }

    private void CreateDatabase()
    {
        var database = new SqlServerDatabase(new SqlConnectionStringBuilder(Database.ConnectionString)
        {
            InitialCatalog = "master"
        }.ToString());

        using var connection = database.Connection();

        using var command = connection.Command($@"

CREATE DATABASE [{Database.Name}]

");

        command.ExecuteNonQuery(minimumNumberOfRowsAffected: -1);
    }

    private void MigrateDatabase()
    {
        using var connection = Database.Connection();

        foreach (var changesetFile in Directory.GetFiles("../../../../ProjetoHexagonal.Persistence.TestSupports/Liquibase/Changesets"))
        {
            using var command = connection.Command(File.ReadAllText(changesetFile));

            command.ExecuteNonQuery(minimumNumberOfRowsAffected: -1);
        }
    }

    private void DestroyDatabase()
    {
        var database = new SqlServerDatabase(new SqlConnectionStringBuilder(Database.ConnectionString)
        {
            InitialCatalog = "master"
        }.ToString());

        using var connection = database.Connection();

        using var command = connection.Command($@"

ALTER DATABASE [{Database.Name}]

SET SINGLE_USER WITH ROLLBACK IMMEDIATE

DROP DATABASE [{Database.Name}]

");

        command.ExecuteNonQuery(minimumNumberOfRowsAffected: -1);
    }
}
