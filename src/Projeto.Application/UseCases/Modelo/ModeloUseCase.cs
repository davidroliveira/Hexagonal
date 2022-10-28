using AutoMapper;
using Projeto.Application.Base;
using Projeto.Application.Models;
using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.Modelo;

public sealed class ModeloUseCase : BaseUseCase<ModeloRequest, ModeloResponse>
{
    private readonly IMapper _mapper;
    private readonly IModeloRepository _modeloRepository;

    public ModeloUseCase(IMapper mapper, IModeloRepository modeloRepository)
    {
        _mapper = mapper;
        _modeloRepository = modeloRepository;
    }

    public override async Task<ModeloResponse> Execute(ModeloRequest request) => await Task.FromResult(
        new ModeloResponse(Task.FromResult(
            _mapper.Map<IEnumerable<ModeloDomain>, IEnumerable<ModeloModel>>(await _modeloRepository.Listar()))));
}