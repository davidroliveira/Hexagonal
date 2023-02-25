namespace ProjetoHexagonal.Commons.Application
{
    public interface IHandler
    {
        TResponse Handle<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
    }
}
