using Projeto.Domain.Domains;

namespace Projeto.Domain.Repositories;

public interface IModeloRepository : IBaseRepository
{
    Task<IEnumerable<ModeloDomain>> Listar();
}