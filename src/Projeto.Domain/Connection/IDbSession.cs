using System.Data;

namespace Projeto.Domain.Connection;

public interface IDbSession
{
    long SessionId { get; }
    Guid ConnectionId { get; }
    IDbConnection? Connection { get; }
    IDbTransaction? Transaction { get; set; }

}