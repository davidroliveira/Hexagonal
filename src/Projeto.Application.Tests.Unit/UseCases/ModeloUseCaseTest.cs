using Projeto.Application.UseCases.Modelo;
using Projeto.Base.Common.Helpers;
using Projeto.Mapper;
using Projeto.Persistence.SqlServer.Repositories;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ModeloUseCaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        var modeloRepository = new ModeloRepository();
        var expected = await modeloRepository.Listar();
        var useCase = new ModeloUseCase(new MapperConfiguration().CreateMapper(), modeloRepository);

        //Act
        var response = await useCase.Execute(new ModeloRequest());

        //Assert
        var result = await response.Content;
        Assert.True(result.Compare(expected));
    }
}