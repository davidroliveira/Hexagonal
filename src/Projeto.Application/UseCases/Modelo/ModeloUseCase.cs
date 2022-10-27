using Projeto.Application.Base;
using Projeto.Domain;

namespace Projeto.Application.UseCases.Modelo;

public sealed class ModeloUseCase : BaseUseCase<ModeloRequest, ModeloResponse>
{
    private readonly IModeloRepository pessoaRepository;

    public ModeloUseCase(IModeloRepository pessoaRepository)
    {
        this.pessoaRepository = pessoaRepository;
    }

    public override Task<ModeloResponse> Execute(ModeloRequest request) => Task.FromResult(new ModeloResponse("Teste 123"));
}