namespace Projeto.Domain.Connection;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}