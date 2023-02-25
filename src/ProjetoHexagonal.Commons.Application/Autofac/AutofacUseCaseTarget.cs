namespace ProjetoHexagonal.Commons.Application.Autofac
{
    public abstract class AutofacUseCaseTarget<TResponse> where TResponse : IResponse
    {
        public abstract TResponse Execute(IRequest<TResponse> request);
    }
}
