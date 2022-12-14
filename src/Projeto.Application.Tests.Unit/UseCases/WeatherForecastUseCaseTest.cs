using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Models;
using Projeto.Application.UseCases.WeatherForecast;
using Projeto.Base.Common.Helpers;
using Projeto.Base.Tests.Support;
using Projeto.Domain.Repositories;
using Projeto.Mapper;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class WeatherForecastUseCaseTest : BaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        await using var app = new TestWebApplicationFactory<Program>();
        using var scope = app.Server.Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IWeatherForecastRepository>();

        var mapper = new MapperConfiguration().CreateMapper();
        var expected = mapper.Map<IEnumerable<WeatherForecastModel>>(await repository.Listar());
        var useCase = new WeatherForecastUseCase(new MapperConfiguration().CreateMapper(), repository);

        //Act
        var response = await useCase.Execute(new WeatherForecastRequest());

        //Assert
        var result = await response.Content;
        Assert.True(result.Compare(expected));
    }
}