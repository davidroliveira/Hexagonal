using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Pessoa.GravarPessoa;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using System.Net;
using System.Net.Mime;
using System.Text;
using Xunit;
using MapperConfiguration = Projeto.Mapper.MapperConfiguration;

namespace Projeto.Driver.Web.Api.Tests.Unit;

public sealed class PessoaControllerTest : BaseTest
{
    [Fact]
    public async Task Get_SeListarExecutadaComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var apiFactory = new TestWebApplicationFactory<Program>();
        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/Pessoa/Listar");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync())!;
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Get_SeGravarExecutadaComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        var expected = new Faker<PessoaModel>("pt_BR")
            .RuleFor(model => model.CodigoUniversal, faker => faker.Random.Guid())
            .RuleFor(model => model.Nome, faker => faker.Person.FirstName)
            .Generate();

        await using var apiFactory = new TestWebApplicationFactory<Program>();

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client
            .PostAsync("/Pessoa/Gravar",
                new StringContent(expected.SerializeAsString(),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json));

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync())!;
        Assert.True(result.Compare(expected));
    }

    [Fact]
    public async Task Get_SeExcluirExecutadaComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var apiFactory = new TestWebApplicationFactory<Program>();
        using var scope = apiFactory.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IPessoaRepository>();

        var mapper = new MapperConfiguration().CreateMapper();

        var expected = new Faker<PessoaModel>("pt_BR")
            .RuleFor(model => model.CodigoUniversal, faker => faker.Random.Guid())
            .RuleFor(model => model.Nome, faker => faker.Person.FirstName)
            .Generate();

        await new GravarPessoaUseCase(mapper, repository).Execute(new GravarPessoaRequest(expected));

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.DeleteAsync($"/Pessoa/Excluir/?codigo={expected.CodigoUniversal.GetValueOrDefault():D}");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync())!;
        Assert.True(result.Compare(expected));
    }
}