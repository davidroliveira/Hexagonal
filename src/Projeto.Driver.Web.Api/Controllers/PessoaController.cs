using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Models;
using Projeto.Application.UseCases.Pessoa.ExcluirPessoa;
using Projeto.Application.UseCases.Pessoa.GravarPessoa;
using Projeto.Application.UseCases.Pessoa.ListarPessoa;
using Projeto.Main;

namespace Projeto.Driver.Web.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class PessoaController : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<PessoaModel>> Listar()
    {
        var response = await Handler.Handle<ListarPessoaUseCase>().Execute(new ListarPessoaRequest());
        return await response.Content;
    }

    [HttpPost]
    public async Task<PessoaModel> Gravar(PessoaModel model)
    {
        var response = await Handler.Handle<GravarPessoaUseCase>().Execute(new GravarPessoaRequest(model));
        return await response.Content;
    }

    [HttpDelete]
    public async Task<PessoaModel?> Excluir(Guid codigo)
    {
        var response = await Handler.Handle<ExcluirPessoaUseCase>().Execute(new ExcluirPessoaRequest(codigo));
        return await response.Content;
    }
}