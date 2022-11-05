using Projeto.Application.Contracts;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Pessoa.ExcluirPessoa;

public sealed record ExcluirPessoaResponse(Task<PessoaModel?> Content) : IResponse;
