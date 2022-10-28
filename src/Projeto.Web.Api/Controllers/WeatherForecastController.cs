using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Models;
using Projeto.Application.UseCases.WeatherForecast;
using Projeto.Main;

namespace Projeto.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecastModel>> Listar()
    {
        var response = await Handler.Handle<WeatherForecastUseCase>().Execute(new WeatherForecastRequest());
        return await response.Content;
    }
}
