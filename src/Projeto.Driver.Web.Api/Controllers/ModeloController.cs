using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Modelo;
using Projeto.Main;

namespace Projeto.Driver.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class ModeloController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ModeloModel>> Listar()
    {
        var response = await Handler.Handle<ModeloUseCase>().Execute(new ModeloRequest());
        return await response.Content;
    }
}