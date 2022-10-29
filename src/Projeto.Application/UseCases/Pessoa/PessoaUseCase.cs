using AutoMapper;
using Projeto.Application.Contracts;
using Projeto.Application.Models;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.Pessoa;

public sealed class PessoaUseCase : IUseCase<PessoaRequest, PessoaResponse>
{
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaUseCase(IMapper mapper, IPessoaRepository pessoaRepository)
    {
        _mapper = mapper;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<PessoaResponse> Execute(PessoaRequest request) => await Task.FromResult(
        new PessoaResponse(Task.FromResult(
            _mapper.Map<IEnumerable<PessoaModel>>(await _pessoaRepository.Listar()))));
}