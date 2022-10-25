using Projeto.Application.UseCases.Modelo;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ModeloUseCaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        var useCase = new ModeloUseCase();

        //Act
        var response = await useCase.Execute(new ModeloRequest());

        //Assert
        Assert.Equal(response.Content, "Teste 123");
    }
}