using AutoMapper;
using Projeto.Application.Contracts;
using Projeto.Application.Models;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.Pessoa.ExcluirPessoa;

public sealed class ExcluirPessoaUseCase : IUseCase<ExcluirPessoaRequest, ExcluirPessoaResponse>
{
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _pessoaRepository;

    public ExcluirPessoaUseCase(IMapper mapper, IPessoaRepository pessoaRepository)
    {
        _mapper = mapper;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<ExcluirPessoaResponse> Execute(ExcluirPessoaRequest request) => await Task.FromResult(
        new ExcluirPessoaResponse(Task.FromResult(
            _mapper.Map<PessoaModel?>(await _pessoaRepository.Excluir(request.Content)))));
}