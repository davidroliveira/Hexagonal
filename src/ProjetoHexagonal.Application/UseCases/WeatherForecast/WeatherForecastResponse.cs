using ProjetoHexagonal.Application.Models;
using ProjetoHexagonal.Commons.Application;

namespace ProjetoHexagonal.Application.UseCases.WeatherForecast;

public sealed record WeatherForecastResponse(WeatherForecastModel[] Result) : IResponse;