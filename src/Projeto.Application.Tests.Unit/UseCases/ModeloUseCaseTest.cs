using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.UseCases.Modelo;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Xunit;
using MapperConfiguration = Projeto.Mapper.MapperConfiguration;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ModeloUseCaseTest : BaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IModeloRepository>();

        var expected = await repository.Listar();
        var useCase = new ModeloUseCase(new MapperConfiguration().CreateMapper(), repository);

        //Act
        var response = await useCase.Execute(new ModeloRequest());

        //Assert
        var result = await response.Content;
        Assert.True(result.Compare(expected));
    }
}