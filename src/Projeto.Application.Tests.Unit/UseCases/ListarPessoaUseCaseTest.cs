using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.UseCases.Pessoa.ListarPessoa;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Xunit;
using MapperConfiguration = Projeto.Mapper.MapperConfiguration;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ListarPessoaUseCaseTest : BaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IPessoaRepository>();

        var mapper = new MapperConfiguration().CreateMapper();

        var useCase = new ListarPessoaUseCase(mapper, repository);

        //Act
        var response = await useCase.Execute(new ListarPessoaRequest());

        //Assert
        var result = await response.Content;
        Assert.NotEmpty(result);
    }
}