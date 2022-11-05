using Projeto.Application.Contracts;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Pessoa.ListarPessoa;

public sealed record ListarPessoaResponse(Task<IEnumerable<PessoaModel>> Content) : IResponse;