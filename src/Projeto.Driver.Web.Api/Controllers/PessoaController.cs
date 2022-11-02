using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Pessoa;
using Projeto.Main;

namespace Projeto.Driver.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class PessoaController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<PessoaModel>> Listar()
    {
        var response = await Handler.Handle<PessoaUseCase>().Execute(new PessoaRequest());
        return await response.Content;
    }
}