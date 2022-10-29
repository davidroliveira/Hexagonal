using Dapper;
using Projeto.Domain.Connection;
using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence.SqlServer.Repositories;

public sealed class PessoaRepository : BaseRepository, IPessoaRepository
{
    private readonly IDbSession _session;

    public PessoaRepository(IDbSession session) => _session = session;

    public Task<IEnumerable<PessoaDomain>> Listar() => _session.Connection.QueryAsync<PessoaDomain>(
        @"select codigo_local_pessoa codigoLocalPessoa, 
                     codigo_universal_pessoa codigoUniversalPessoa, 
                     nome_pessoa nomePessoa
                from PESSOA",
        null,
        _session.Transaction);
}