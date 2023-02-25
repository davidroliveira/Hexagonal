using ProjetoHexagonal.Persistence.TestSupports;
using NUnit.Framework;

namespace ProjetoHexagonal.Application.UnitTests;

public abstract class ApplicationUnitTests
{
    private readonly DatabaseFixture databaseFixture;
    protected string ConnectionString => databaseFixture.Database.ConnectionString;

    protected ApplicationUnitTests() => databaseFixture = DatabaseFixture.FromEnvironmentOrDefault();

    [SetUp]
    public void SetUp() => databaseFixture.SetUp();

    [TearDown]
    public void TearDown() => databaseFixture.TearDown();
}