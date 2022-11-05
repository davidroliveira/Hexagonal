using AutoMapper;
using Projeto.Application.Contracts;
using Projeto.Application.Models;
using Projeto.Domain.Repositories;

namespace Projeto.Application.UseCases.Pessoa.ListarPessoa;

public sealed class ListarPessoaUseCase : IUseCase<ListarPessoaRequest, ListarPessoaResponse>
{
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _pessoaRepository;

    public ListarPessoaUseCase(IMapper mapper, IPessoaRepository pessoaRepository)
    {
        _mapper = mapper;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<ListarPessoaResponse> Execute(ListarPessoaRequest request) => await Task.FromResult(
        new ListarPessoaResponse(Task.FromResult(
            _mapper.Map<IEnumerable<PessoaModel>>(await _pessoaRepository.Listar()))));
}