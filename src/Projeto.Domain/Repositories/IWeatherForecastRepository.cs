using Projeto.Domain.Domains;

namespace Projeto.Domain.Repositories;

public interface IWeatherForecastRepository : IBaseRepository
{
    Task<IEnumerable<WeatherForecastDomain>> Listar();
}