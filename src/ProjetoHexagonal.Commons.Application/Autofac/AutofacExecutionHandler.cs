using Autofac;

namespace ProjetoHexagonal.Commons.Application.Autofac
{
    public sealed class AutofacExecutionHandler : IHandler
    {
        private readonly ILifetimeScope scope;

        public AutofacExecutionHandler(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public TResponse Handle<TResponse>(IRequest<TResponse> request) where TResponse : IResponse
        {
            return scope.Resolve<AutofacUseCaseTarget<TResponse>>().Execute(request);
        }
    }
}
