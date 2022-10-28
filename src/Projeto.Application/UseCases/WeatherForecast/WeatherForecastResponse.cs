using Projeto.Application.Base;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.WeatherForecast;

public sealed record WeatherForecastResponse(Task<IEnumerable<WeatherForecastModel>> Content) : BaseResponse;