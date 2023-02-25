using ProjetoHexagonal.Application.UseCases.Modelo;
using ProjetoHexagonal.Commons.Application;
using Microsoft.AspNetCore.Components;

namespace ProjetoHexagonal.Web.Blazor.Pages;

public partial class Modelo
{
    private string retorno = string.Empty;

    [Inject]
    private IHandler Handler { get; set; } = default!;

    private void ExecuteClick()
    {
        retorno = Handler.Handle(new ModeloRequest()).Result;
    }
}