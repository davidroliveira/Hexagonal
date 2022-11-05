using Projeto.Application.Contracts;

namespace Projeto.Application.UseCases.Pessoa.ExcluirPessoa;

public sealed record ExcluirPessoaRequest(Guid Content) : IRequest;