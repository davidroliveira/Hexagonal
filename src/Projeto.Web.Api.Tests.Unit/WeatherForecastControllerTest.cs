using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using Xunit;

namespace Projeto.Web.Api.Tests.Unit;

public sealed class WeatherForecastControllerTest
{
    [Fact]
    public async Task Get_SeListarExecutadaComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var apiFactory = new WebApplicationFactory<Program>();

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/WeatherForecast/Listar");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var lista = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync())!;
        Assert.NotEmpty(lista);
    }
}