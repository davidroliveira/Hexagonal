using Projeto.Application.Contracts;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Pessoa.GravarPessoa;

public sealed record GravarPessoaResponse(Task<PessoaModel> Content) : IResponse;