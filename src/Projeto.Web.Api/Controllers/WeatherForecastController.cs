using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Models;
using Projeto.Application.UseCases.WeatherForecast;

namespace Projeto.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecastModel>> Listar()
    {
        var response = await new WeatherForecastUseCase().Execute(new WeatherForecastRequest());
        return response.Content;
    }
}
