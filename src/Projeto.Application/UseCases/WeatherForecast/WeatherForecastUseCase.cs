using Projeto.Application.Base;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.WeatherForecast;

public sealed class WeatherForecastUseCase : BaseUseCase<WeatherForecastRequest, WeatherForecastResponse>
{
    private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public override Task<WeatherForecastResponse> Execute(WeatherForecastRequest request)
    {
        return Task.FromResult(new WeatherForecastResponse(Enumerable.Range(1, 5).Select(index => new WeatherForecastModel()
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })));
    }
}