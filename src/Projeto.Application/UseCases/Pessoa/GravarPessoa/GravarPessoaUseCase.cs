using AutoMapper;
using Projeto.Application.Contracts;
using Projeto.Application.Models;
using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.Pessoa.GravarPessoa;

public sealed class GravarPessoaUseCase : IUseCase<GravarPessoaRequest, GravarPessoaResponse>
{
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _pessoaRepository;

    public GravarPessoaUseCase(IMapper mapper, IPessoaRepository pessoaRepository)
    {
        _mapper = mapper;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<GravarPessoaResponse> Execute(GravarPessoaRequest request)
    {
        var domain = _mapper.Map<PessoaDomain>(request.Content);
        return await Task.FromResult(
            new GravarPessoaResponse(Task.FromResult(
                _mapper.Map<PessoaModel>(await _pessoaRepository.Gravar(domain)))));
    }
}