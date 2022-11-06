using Microsoft.JSInterop;
using Projeto.Application.Models;
using Projeto.Base.Common.Helpers;
using System.Net.Http.Json;

namespace Projeto.Driver.Blazor.Pages;

public partial class Pessoa
{
    private static Pessoa _instance = null!;
    private static IJSRuntime _jsRuntime = null!;
    private static HttpClient _http = null!;
    private static List<PessoaModel>? _listaPessoas;
    private PessoaModel _pessoaSelecionada = new();
    private string _tituloModal = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        _instance = this;
        _http = Http;
        _listaPessoas = (await Http.GetFromJsonAsync<List<PessoaModel>>("https://localhost:44358/Pessoa/Listar"))!;
        _jsRuntime = JsRuntime;
    }

    private async Task IncluirOnClick()
    {
        _tituloModal = "Incluir Pessoa";
        _pessoaSelecionada = new();
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Incluir", _pessoaSelecionada.CodigoUniversal);
    }

    private async Task EditarOnClick(PessoaModel pessoa)
    {
        _tituloModal = "Alterar Pessoa";
        _pessoaSelecionada = pessoa.Clone<PessoaModel>()!;
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Editar", pessoa.CodigoUniversal);
    }

    private async Task ExcluirOnClick(PessoaModel pessoa)
    {
        await JsRuntime.InvokeVoidAsync("MethodCstoJsCall", "Excluir", pessoa.CodigoUniversal);
        await JsRuntime.InvokeVoidAsync("messageConfirma", pessoa);
    }

    private Task AddOrUpdadeList()
    {
        var index = _listaPessoas!.FindIndex(model => model.CodigoUniversal.Equals(_pessoaSelecionada.CodigoUniversal));
        if (index == -1)
        {
            _listaPessoas.Add(_pessoaSelecionada);
            return Task.CompletedTask;
        }

        _listaPessoas[index] = _pessoaSelecionada;
        return Task.CompletedTask;
    }

    private async Task SaveOnClick()
    {
        using var response = await Http.PostAsJsonAsync("https://localhost:44358/Pessoa/Gravar", _pessoaSelecionada);

        _pessoaSelecionada = (await response.Content.ReadFromJsonAsync<PessoaModel>())!;
        await AddOrUpdadeList();
        await JsRuntime.InvokeVoidAsync("closeModal");
    }

    [JSInvokable]
    public static async Task MethodJstoCsCall(string nameEvent, string codigo)
    {
        var mensagem = $"Executou evento {nameEvent}, codigo {codigo}";
        await _jsRuntime.InvokeVoidAsync("exibeMensagem", mensagem);
    }

    [JSInvokable]
    public static async Task ExclusaoConfirmada(PessoaModel pessoa)
    {
        using var response = await _http.DeleteAsync($"https://localhost:44358/Pessoa/Excluir/?codigo={pessoa.CodigoUniversal:D}");
        
        var pessoaSelecionada = _listaPessoas!.FirstOrDefault(model => model.CodigoUniversal.Equals(pessoa.CodigoUniversal))!;

        var mensagem = "NÃO achou na lista!!!";
        if (_listaPessoas!.Remove(pessoaSelecionada))
            mensagem = "achou na lista!!!";
        await _jsRuntime.InvokeVoidAsync("exibeMensagem", mensagem);

        await _jsRuntime.InvokeVoidAsync("exibeMensagem", "exclusão!!!");
        _instance.StateHasChanged();
    }
}