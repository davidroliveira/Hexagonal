using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Pessoa;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Xunit;
using MapperConfiguration = Projeto.Mapper.MapperConfiguration;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class PessoaUseCaseTest : BaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IPessoaRepository>();

        var mapper = new MapperConfiguration().CreateMapper();
        var expected = mapper.Map<IEnumerable<PessoaModel>>(await repository.Listar());
        var useCase = new PessoaUseCase(mapper, repository);

        //Act
        var response = await useCase.Execute(new PessoaRequest());

        //Assert
        var result = await response.Content;
        Assert.True(result.Compare(expected));
    }
}