using ProjetoHexagonal.Domain.Domains;

namespace ProjetoHexagonal.Domain.Repositories;

public interface IWeatherForecastRepository
{
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
}