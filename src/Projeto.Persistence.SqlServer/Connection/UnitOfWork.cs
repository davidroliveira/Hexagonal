using Projeto.Domain.Connection;

namespace Projeto.Persistence.SqlServer.Connection;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IDbSession _session;

    public UnitOfWork(IDbSession session) => _session = session;

    public void BeginTransaction() => _session.Transaction = _session.Connection.BeginTransaction();

    public void CommitTransaction()
    {
        _session.Transaction.Commit();
        Dispose();
    }

    public void RollbackTransaction()
    {
        _session.Transaction.Rollback();
        Dispose();
    }

    public void Dispose() => _session.Transaction.Dispose();
}