using Microsoft.JSInterop;
using Projeto.Application.Models;
using Projeto.Base.Common.Helpers;
using System.Net.Http.Json;

namespace Projeto.Driver.Blazor.Pages;

public partial class Pessoa
{
    private static IJSRuntime? _jsRuntime;
    private List<PessoaModel>? _listaPessoas;
    private PessoaModel _pessoaSelecionada = new();

    protected override async Task OnInitializedAsync()
    {
        _listaPessoas = await Http.GetFromJsonAsync<List<PessoaModel>>("https://localhost:44358/Pessoa/Listar");
        _jsRuntime = JsRuntime;
    }

    private async Task EditarOnClick(PessoaModel pessoa)
    {
        _pessoaSelecionada = pessoa.Clone<PessoaModel>()!;
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Editar", pessoa.CodigoUniversal);
    }

    private async Task ExcluirOnClick(PessoaModel pessoa)
    {
        using var response = await Http.DeleteAsync($"https://localhost:44358/Pessoa/Excluir/?codigo={pessoa.CodigoUniversal.GetValueOrDefault():D}");
        _listaPessoas?.Remove(pessoa);
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Excluir", pessoa.CodigoUniversal);
    }

    private async Task SaveOnClick()
    {
        using var response = await Http.PostAsJsonAsync("https://localhost:44358/Pessoa/Gravar", _pessoaSelecionada);
        _pessoaSelecionada = (await response.Content.ReadFromJsonAsync<PessoaModel>())!;
        _listaPessoas![_listaPessoas.FindIndex(model => model.CodigoUniversal.GetValueOrDefault().Equals(_pessoaSelecionada.CodigoUniversal.GetValueOrDefault()))] = _pessoaSelecionada;
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