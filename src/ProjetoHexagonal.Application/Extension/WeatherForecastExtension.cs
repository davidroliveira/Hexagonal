using ProjetoHexagonal.Application.Models;
using ProjetoHexagonal.Domain.Domains;

namespace ProjetoHexagonal.Application.Extension;

public static class WeatherForecastExtension
{
    public static WeatherForecastModel AsModel(this WeatherForecast self) => new(self.Date, self.TemperatureC, self.TemperatureF, self.Summary);
}