using ProjetoHexagonal.Commons.Application;
using ProjetoHexagonal.Domain.Repositories;

namespace ProjetoHexagonal.Application.UseCases.Modelo;

public sealed class ModeloUseCase : IUseCase<ModeloRequest, ModeloResponse>
{
    private readonly IModeloRepository modeloRepository;

    public ModeloUseCase(IModeloRepository modeloRepository) => this.modeloRepository = modeloRepository;

    public ModeloResponse Execute(ModeloRequest request) => new(modeloRepository.ConsultaMensagemModelo(1) + " - " + DateTime.Now.ToString("F"));
}