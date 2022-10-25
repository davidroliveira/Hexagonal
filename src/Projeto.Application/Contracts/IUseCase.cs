namespace Projeto.Application.Contracts;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IRequest
    where TResponse : IResponse
{
    Task<TResponse> Execute(TRequest request);
}