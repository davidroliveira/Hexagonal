using Dapper;
using Projeto.Domain.Connection;
using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence.SqlServer.Repositories;

public sealed class PessoaRepository : BaseRepository, IPessoaRepository
{
    private readonly IDbSession _session;

    public PessoaRepository(IDbSession session) => _session = session;

    public Task<IEnumerable<PessoaDomain>> Listar() => _session.Connection.QueryAsync<PessoaDomain>(@"
                 set transaction isolation level read uncommitted; 
              select codigo_local codigoLocal, 
                     codigo_universal codigoUniversal, 
                     nome Nome
                from pessoas",
        null,
        _session.Transaction);
}