namespace Projeto.Application.Models;

public sealed record WeatherForecastModel(
    DateTime Date,
    int TemperatureC,
    string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}