namespace Projeto.Domain.Domains;

public sealed class WeatherForecastDomain : BaseDomain
{
    public WeatherForecastDomain(
        DateTime date,
        int temperatureC,
        string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public DateTime Date { get; }

    public int TemperatureC { get; }

    public string? Summary { get; }
}