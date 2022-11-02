namespace Projeto.Application.Models;

public sealed record PessoaModel(Guid? CodigoUniversal, string Nome) : BaseModel;