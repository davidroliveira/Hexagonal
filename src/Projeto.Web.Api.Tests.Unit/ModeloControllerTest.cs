using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Projeto.Web.Api.Tests.Unit;

public sealed class ModeloControllerTest
{
    [Fact]
    public async Task Get_SeTesteExecutadoComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var apiFactory = new WebApplicationFactory<Program>();

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/Modelo/Teste");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(await response.Content.ReadAsStringAsync(), "Teste 123");
    }
}