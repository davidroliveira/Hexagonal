namespace Projeto.Domain.Domains;

public sealed class PessoaDomain : BaseDomain
{
    public long? CodigoLocal { get; set; }
    public Guid CodigoUniversal { get; set; }
    public string Nome { get; set; } = null!;
}