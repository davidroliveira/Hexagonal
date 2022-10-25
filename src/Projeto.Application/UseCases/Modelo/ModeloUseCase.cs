using Projeto.Application.Base;

namespace Projeto.Application.UseCases.Modelo;

public sealed class ModeloUseCase : BaseUseCase<ModeloRequest, ModeloResponse>
{
    public override Task<ModeloResponse> Execute(ModeloRequest request) => Task.FromResult(new ModeloResponse("Teste 123"));
}