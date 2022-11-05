using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Projeto.Application.Models;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Projeto.Mapper;
using System.Net;
using Xunit;

namespace Projeto.Driver.Web.Api.Tests.Unit;

public sealed class WeatherForecastControllerTest : BaseTest
{
    [Fact]
    public async Task Get_SeListarExecutadaComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        await using var apiFactory = new TestWebApplicationFactory<Program>();
        using var scope = apiFactory.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IWeatherForecastRepository>();

        var mapper = new MapperConfiguration().CreateMapper();
        var expected = mapper.Map<IEnumerable<WeatherForecastModel>>(await repository.Listar());

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/WeatherForecast/Listar");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync())!;
        Assert.True(result.Compare(expected));
    }
}