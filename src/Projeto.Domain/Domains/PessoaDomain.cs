namespace Projeto.Domain.Domains;

public sealed class PessoaDomain : BaseDomain
{
    public PessoaDomain(
        long? codigoLocal,
        Guid codigoUniversal,
        string nome)
    {
        CodigoLocal = codigoLocal;
        CodigoUniversal = codigoUniversal;
        Nome = nome;
    }

    public long? CodigoLocal { get; }
    public Guid CodigoUniversal { get; }
    public string Nome { get; }
}