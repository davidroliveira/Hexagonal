using ProjetoHexagonal.Application.Extension;
using ProjetoHexagonal.Commons.Application;
using ProjetoHexagonal.Domain.Repositories;

namespace ProjetoHexagonal.Application.UseCases.WeatherForecast;

public sealed class WeatherForecastUseCase : IUseCase<WeatherForecastRequest, WeatherForecastResponse>
{
    private readonly IWeatherForecastRepository weatherForecastRepository;

    public WeatherForecastUseCase(IWeatherForecastRepository weatherForecastRepository) => this.weatherForecastRepository = weatherForecastRepository;

    public WeatherForecastResponse Execute(WeatherForecastRequest request)
    {
        var result = weatherForecastRepository
            .GetForecastAsync(DateTime.Now)
            .GetAwaiter()
            .GetResult()
            .ToList()
            .Select(weatherForecast => weatherForecast.AsModel())
            .ToArray();

        return new WeatherForecastResponse(result);
    }
}