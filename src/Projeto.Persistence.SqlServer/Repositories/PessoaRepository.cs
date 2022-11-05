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
        from pessoas");

    public Task<PessoaDomain> Gravar(PessoaDomain domain) => _session.Connection.QuerySingleAsync<PessoaDomain>($@"
          with cte
            as (select @codigo_universal codigo_universal,
                       @nome nome)
         merge 
           pessoas as Destino
         using 
           cte AS Origem on (Origem.codigo_universal = Destino.codigo_universal)
          when matched then
               update set Destino.codigo_universal = Origem.codigo_universal, 
                          Destino.nome = Origem.nome
          when not matched then
               insert (codigo_universal, 
                       nome)
               values (Origem.codigo_universal, 
                       Origem.nome)
               output inserted.codigo_local CodigoLocal,
                      inserted.codigo_universal CodigoUniversal,
                      inserted.nome Nome;",
            new Dictionary<string, object>
            {
                { "@codigo_universal", domain.CodigoUniversal },
                { "@nome", domain.Nome }
            });

    public Task<PessoaDomain?> Excluir(Guid codigoUniversal) => _session.Connection.QuerySingleOrDefaultAsync<PessoaDomain?>(@"
      delete pessoas
      output deleted.codigo_local codigoLocal, 
             deleted.codigo_universal codigoUniversal, 
             deleted.nome Nome
       where codigo_universal = @codigo_universal",
        new Dictionary<string, object>
        {
            { "@codigo_universal", codigoUniversal }
        });
}