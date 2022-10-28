using Projeto.Application.UseCases.WeatherForecast;
using Projeto.Mapper;
using Projeto.Persistence.SqlServer.Repositories;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class WeatherForecastUseCaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        var useCase = new WeatherForecastUseCase(new MapperConfiguration().CreateMapper(), new WeatherForecastRepository());

        //Act
        var response = await useCase.Execute(new WeatherForecastRequest());

        //Assert
        Assert.NotEmpty(await response.Content);
    }
}