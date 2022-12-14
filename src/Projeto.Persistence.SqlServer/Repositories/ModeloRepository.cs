using Projeto.Domain.Domains;
using Projeto.Domain.Repositories;

namespace Projeto.Persistence.SqlServer.Repositories;

public sealed class ModeloRepository : BaseRepository, IModeloRepository
{
    private static IEnumerable<ModeloDomain> Lista => new[] { new ModeloDomain { Codigo = 1, Nome = "Teste" } };

    public Task<IEnumerable<ModeloDomain>> Listar() => Task.FromResult(Lista);
}