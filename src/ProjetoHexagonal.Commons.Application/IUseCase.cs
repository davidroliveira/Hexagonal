namespace ProjetoHexagonal.Commons.Application
{
    public interface IUseCase<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        TResponse Execute(TRequest request);
    }
}
