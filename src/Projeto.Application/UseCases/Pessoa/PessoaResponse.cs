using Projeto.Application.Contracts;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Pessoa;

public sealed record PessoaResponse(Task<IEnumerable<PessoaModel>> Content) : IResponse;