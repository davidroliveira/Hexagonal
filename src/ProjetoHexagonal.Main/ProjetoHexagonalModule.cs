using Autofac;
using ProjetoHexagonal.Application.UseCases.Modelo;
using ProjetoHexagonal.Application.UseCases.WeatherForecast;
using ProjetoHexagonal.Commons.Application;
using ProjetoHexagonal.Commons.Application.Autofac;
using ProjetoHexagonal.Domain.Repositories;
using ProjetoHexagonal.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace ProjetoHexagonal.Main;

public sealed class ProjetoHexagonalModule : Module
{
    private readonly IConfiguration config;

    public ProjetoHexagonalModule(IConfiguration config) => this.config = config;

    protected override void Load(ContainerBuilder builder)
    {
        RegisterRepositories(builder);
        RegisterUseCases(builder);
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        var connectionString = config.GetConnectionString("Default")!;

        builder
            .Register(_ => new ModeloRepository(connectionString))
            .As<IModeloRepository>()
            .InstancePerLifetimeScope();

        builder
            .Register(_ => new WeatherForecastRepository())
            .As<IWeatherForecastRepository>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterUseCases(ContainerBuilder builder)
    {
        RegisterUseCase<ModeloUseCase, ModeloRequest, ModeloResponse>(builder);
        RegisterUseCase<WeatherForecastUseCase, WeatherForecastRequest, WeatherForecastResponse>(builder);
    }

    private static void RegisterUseCase<TUseCase, TRequest, TResponse>(ContainerBuilder builder)
        where TUseCase : IUseCase<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse
    {
        builder.RegisterType<TUseCase>()
            .As<IUseCase<TRequest, TResponse>>()
            .InstancePerDependency();

        builder.Register(c => new AutofacUseCaseAdapter<TRequest, TResponse>(c.Resolve<IUseCase<TRequest, TResponse>>()))
            .As<AutofacUseCaseTarget<TResponse>>()
            .InstancePerDependency();
    }
}
