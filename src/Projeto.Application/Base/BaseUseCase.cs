using Projeto.Application.Contracts;

namespace Projeto.Application.Base;

public abstract class BaseUseCase<TRequest, TResponse> : IUseCase<TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : IResponse
{
    public abstract Task<TResponse> Execute(TRequest request);
}