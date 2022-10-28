using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence;

public sealed class WeatherForecastRepository : BaseRepository, IWeatherForecastRepository
{
    private static readonly string[] Summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public Task<IEnumerable<WeatherForecastDomain>> Listar() => Task.FromResult(Enumerable.Range(1, 5)
        .Select(index => new WeatherForecastDomain(
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)])));
}