using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence;

public sealed class ModeloRepository : BaseRepository, IModeloRepository
{
    private static IEnumerable<ModeloDomain> Lista => new[] { new ModeloDomain(1, "Teste") };

    public Task<IEnumerable<ModeloDomain>> Listar() => Task.FromResult(Lista);
}