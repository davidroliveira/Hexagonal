namespace Projeto.Application.Models;

public sealed class PessoaModel : BaseModel
{
    public Guid? CodigoUniversal { get; set; } = Guid.NewGuid();
    public string Nome { get; set; } = string.Empty;
}