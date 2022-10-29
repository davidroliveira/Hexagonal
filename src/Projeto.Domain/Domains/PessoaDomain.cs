namespace Projeto.Domain.Domains;

public sealed class PessoaDomain : BaseDomain
{
    public PessoaDomain(
        long? codigoLocalPessoa,
        Guid codigoUniversalPessoa,
        string nomePessoa)
    {
        CodigoLocalPessoa = codigoLocalPessoa;
        CodigoUniversalPessoa = codigoUniversalPessoa;
        NomePessoa = nomePessoa;
    }

    public long? CodigoLocalPessoa { get; }
    public Guid CodigoUniversalPessoa { get; }
    public string NomePessoa { get; }
}