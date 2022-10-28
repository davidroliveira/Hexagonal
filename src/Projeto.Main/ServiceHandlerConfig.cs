using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Contracts;
using Projeto.Persistence.SqlServer.Repositories;
using System.Reflection;

namespace Projeto.Main;

public static class ServiceHandlerConfig
{
    public static IServiceCollection AddHandlerConfig(this IServiceCollection services)
    {
        RegisterUseCases(services).GetAwaiter();
        RegisterRepositories(services).GetAwaiter();
        Handler.CurrentProvider = services.BuildServiceProvider();
        return services;
    }

    private static Task RegisterRepositories(IServiceCollection services) => Task.Run(() => Assembly
        .GetAssembly(typeof(BaseRepository))!
        .GetTypes()
        .Where(type => type.IsSealed && type.IsSubclassOf(typeof(BaseRepository)))
        .ToList()
        .ForEach(type => services.AddTransient(type.GetInterfaces()
            .ToList()
            .FirstOrDefault(typeInterfaced => typeInterfaced.GetInterfaces().Any())!, type)));

    private static Task RegisterUseCases(IServiceCollection services) => Task.Run(() => Assembly
        .GetAssembly(typeof(IUseCase<IRequest, IResponse>))!
        .GetTypes()
        .Where(type =>
            type.IsSealed &&
            type.GetInterfaces().ToList().Any(typeInterfaced =>
                (typeInterfaced.Namespace?.Equals(typeof(IUseCase<IRequest, IResponse>).Namespace) ?? false) &&
                typeInterfaced.Name.Equals(typeof(IUseCase<IRequest, IResponse>).Name)))
        .ToList()
        .ForEach(type => services.AddTransient(type)));
}