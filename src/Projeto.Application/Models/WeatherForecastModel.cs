namespace Projeto.Application.Models;

public sealed record WeatherForecastModel(DateTime Date, int TemperatureC, string? Summary) : BaseModel
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}