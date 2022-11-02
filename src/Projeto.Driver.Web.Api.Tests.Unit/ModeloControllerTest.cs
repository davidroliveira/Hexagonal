using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using System.Net;
using Xunit;

namespace Projeto.Driver.Web.Api.Tests.Unit;

public sealed class ModeloControllerTest : BaseTest
{
    [Fact]
    public async Task Get_SeTesteExecutadoComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IModeloRepository>();

        var expected = await repository.Listar();
        await using var apiFactory = new TestWebApplicationFactory<Program>();

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/Modelo/Listar");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync())!;
        Assert.True(result.Compare(expected));
    }
}