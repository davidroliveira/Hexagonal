namespace ProjetoHexagonal.Commons.Application.Autofac
{
    public sealed class AutofacUseCaseAdapter<TRequest, TResponse> : AutofacUseCaseTarget<TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        private readonly IUseCase<TRequest, TResponse> useCaseAdaptee;

        public AutofacUseCaseAdapter(IUseCase<TRequest, TResponse> useCaseAdaptee)
        {
            this.useCaseAdaptee = useCaseAdaptee;
        }

        public override TResponse Execute(IRequest<TResponse> request)
        {
            return Execute((TRequest)request);
        }

        private TResponse Execute(TRequest request)
        {
            return useCaseAdaptee.Execute(request);
        }
    }
}
