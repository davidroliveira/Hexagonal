using Microsoft.AspNetCore.Mvc;
using Projeto.Application.UseCases.Modelo;

namespace Projeto.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class ModeloController : ControllerBase
{
    [HttpGet]
    public async Task<string> Teste()
    {
        var response = await new ModeloUseCase().Execute(new ModeloRequest());
        return response.Content;
    }
}