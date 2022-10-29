using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence.SqlServer.Repositories;

public sealed class WeatherForecastRepository : BaseRepository, IWeatherForecastRepository
{
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public Task<IEnumerable<WeatherForecastDomain>> Listar() => Task.FromResult(Enumerable.Range(1, Summaries.Length)
        .Select(index => new WeatherForecastDomain(
            DateTime.Now.AddDays(index).Date,
            10 * index,
            Summaries[index - 1])));
}