﻿using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Pessoa.ExcluirPessoa;
using Projeto.Application.UseCases.Pessoa.GravarPessoa;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Xunit;
using MapperConfiguration = Projeto.Mapper.MapperConfiguration;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class ExcluirPessoaUseCaseTest : BaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IPessoaRepository>();

        var mapper = new MapperConfiguration().CreateMapper();

        var expected = new Faker<PessoaModel>("pt_BR")
            .RuleFor(model => model.CodigoUniversal, faker => faker.Random.Guid())
            .RuleFor(model => model.Nome, faker => faker.Person.FirstName)
            .Generate();

        await new GravarPessoaUseCase(mapper, repository).Execute(new GravarPessoaRequest(expected));

        var useCase = new ExcluirPessoaUseCase(mapper, repository);

        //Act
        var response = await useCase.Execute(new ExcluirPessoaRequest(expected.CodigoUniversal.GetValueOrDefault()));

        //Assert
        var result = await response.Content;
        Assert.True(result!.Compare(expected));
    }
}