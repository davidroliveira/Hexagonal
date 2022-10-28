namespace Projeto.Domain.Domains;

public sealed class ModeloDomain : BaseDomain
{
    public ModeloDomain(
        long codigo,
        string nome)
    {
        Codigo = codigo;
        Nome = nome;
    }

    public long Codigo { get; }
    public string Nome { get; }

}