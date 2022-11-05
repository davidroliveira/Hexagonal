using Microsoft.JSInterop;
using Projeto.Application.Models;
using System.Net.Http.Json;

namespace Projeto.Driver.Blazor.Pages;

public partial class Pessoa
{
    private static IJSRuntime? _jsRuntime;
    private List<PessoaModel>? _listaPessoas;
    private PessoaModel _pessoaSelecionada = new();

    protected override async Task OnInitializedAsync()
    {
        _listaPessoas = await Http.GetFromJsonAsync<List<PessoaModel>>("https://localhost:7176/Pessoa/Listar");
        _jsRuntime = JsRuntime;
    }

    private async Task EditarOnClick(PessoaModel pessoa)
    {
        _pessoaSelecionada = pessoa;
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Editar", pessoa.CodigoUniversal);
    }

    private async Task ExcluirOnClick(PessoaModel pessoa)
    {
        _listaPessoas?.Remove(pessoa);
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Excluir", pessoa.CodigoUniversal);
    }

    private async Task SaveOnClick()
    {

        await JsRuntime.InvokeVoidAsync("closeModal");
    }


    [JSInvokable]
    public static Task MethodJstoCsCall(string nameEvent, string codigo)
    {
        var mensagem = $"Executou evento {nameEvent}, codigo {codigo}";
        _jsRuntime?.InvokeVoidAsync("exibeMensagem", mensagem).GetAwaiter();
        return Task.CompletedTask;
    }
}