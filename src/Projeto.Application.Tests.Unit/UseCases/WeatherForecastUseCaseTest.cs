using Projeto.Application.UseCases.WeatherForecast;
using Xunit;

namespace Projeto.Application.Tests.Unit.UseCases;

public sealed class WeatherForecastUseCaseTest
{
    [Fact]
    public async Task Execute_SeExecutadoComSucesso_EntaoRetornaResponse()
    {
        //Arrange
        var useCase = new WeatherForecastUseCase();

        //Act
        var response = await useCase.Execute(new WeatherForecastRequest());

        //Assert
        Assert.NotEmpty(response.Content);
    }
}