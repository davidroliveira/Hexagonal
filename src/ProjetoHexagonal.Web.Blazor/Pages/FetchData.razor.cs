using ProjetoHexagonal.Application.Models;
using ProjetoHexagonal.Application.UseCases.WeatherForecast;
using ProjetoHexagonal.Commons.Application;
using Microsoft.AspNetCore.Components;

namespace ProjetoHexagonal.Web.Blazor.Pages;

public partial class FetchData
{
    private WeatherForecastModel[]? forecasts;

    [Inject]
    private IHandler Handler { get; set; } = default!;

    protected override Task OnInitializedAsync()
    {
        forecasts = Handler.Handle(new WeatherForecastRequest(DateTime.Now)).Result;
        return base.OnInitializedAsync();
    }
}