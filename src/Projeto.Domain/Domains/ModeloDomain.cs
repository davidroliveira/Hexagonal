namespace Projeto.Domain.Domains;

public sealed class ModeloDomain : BaseDomain
{
    public long Codigo { get; set; }
    public string Nome { get; set; } = null!;
}