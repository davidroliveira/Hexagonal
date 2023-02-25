using Microsoft.AspNetCore.Components;

namespace ProjetoHexagonal.Web.Blazor.Shared;

public sealed partial class SurveyPrompt
{
    // Demonstrates how a parent component can supply parameters
    [Parameter]
    public string? Title { get; set; }
}