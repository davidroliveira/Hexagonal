using Projeto.Application.UseCases.Modelo;
using Projeto.Mapper;
using Projeto.Persistence;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ModeloUseCaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        var useCase = new ModeloUseCase(new MapperConfiguration().CreateMapper(), new ModeloRepository());

        //Act
        var response = await useCase.Execute(new ModeloRequest());

        //Assert
        Assert.NotEmpty(await response.Content);
    }
}