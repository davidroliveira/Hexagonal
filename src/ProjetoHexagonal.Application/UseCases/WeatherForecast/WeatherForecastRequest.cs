using ProjetoHexagonal.Commons.Application;

namespace ProjetoHexagonal.Application.UseCases.WeatherForecast;

public sealed record WeatherForecastRequest(DateTime StartDate) : IRequest<WeatherForecastResponse>;