namespace Projeto.Domain.Domains;

public sealed class WeatherForecastDomain : BaseDomain
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}