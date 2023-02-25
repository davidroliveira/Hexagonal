using ProjetoHexagonal.Application.UseCases.WeatherForecast;
using ProjetoHexagonal.Persistence.Repositories;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ProjetoHexagonal.Application.UnitTests.UseCases;

[TestFixture]
public sealed class WeatherForecastTests : ApplicationUnitTests
{
    [Test]
    public void Execute_SeWeatherForecastExecutadoComSucesso_EntaoRetornaArray()
    {
        // Arrange
        var weatherForecastRepository = new WeatherForecastRepository();
        var useCase = new WeatherForecastUseCase(weatherForecastRepository);

        // Act
        var result = useCase.Execute(new WeatherForecastRequest(DateTime.Now));
        var actual = result.Result;

        // Assert
        Console.WriteLine("actual: " + JsonConvert.SerializeObject(actual));
        CollectionAssert.IsNotEmpty(actual);
    }
}