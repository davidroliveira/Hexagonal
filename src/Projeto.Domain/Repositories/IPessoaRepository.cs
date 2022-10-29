using Projeto.Domain.Domains;

namespace Projeto.Domain.Repositories;

public interface IPessoaRepository : IBaseRepository
{
    Task<IEnumerable<PessoaDomain>> Listar();
}