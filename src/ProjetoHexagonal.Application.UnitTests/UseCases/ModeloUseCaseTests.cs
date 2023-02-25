using ProjetoHexagonal.Application.UseCases.Modelo;
using ProjetoHexagonal.Persistence.Repositories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ProjetoHexagonal.Application.UnitTests.UseCases;

[TestFixture]
public sealed class ModeloUseCaseTests : ApplicationUnitTests
{
    [Test]
    public void Execute_SeModeloExecutadoComSucesso_EntaoRetornaMensagem()
    {
        // Arrange
        var modeloRepository = new ModeloRepository(ConnectionString);
        var expected = modeloRepository.ConsultaMensagemModelo(1) + " - " + DateTime.Now.ToString("F");
        var useCase = new ModeloUseCase(modeloRepository);

        // Act
        var result = useCase.Execute(new ModeloRequest());
        var actual = result.Result;

        // Assert
        Console.WriteLine("expected: " + JsonConvert.SerializeObject(expected));
        Console.WriteLine("actual: " + JsonConvert.SerializeObject(actual));
        Assert.That(actual, Is.EqualTo(expected));
    }
}