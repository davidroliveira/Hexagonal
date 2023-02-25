using ProjetoHexagonal.Domain.Domains;
using ProjetoHexagonal.Domain.Repositories;

namespace ProjetoHexagonal.Persistence.Repositories;

public sealed class WeatherForecastRepository : IWeatherForecastRepository
{
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate) => Task.FromResult(
        Enumerable
            .Range(1, 5)
            .Select(index => new WeatherForecast(
                startDate.AddDays(index),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)])).ToArray());
}