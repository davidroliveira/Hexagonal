﻿using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Contracts;
using Projeto.Domain.Connection;
using Projeto.Persistence.SqlServer.Connection;
using Projeto.Persistence.SqlServer.Repositories;
using System.Reflection;

namespace Projeto.Main;

public static class ServiceHandlerConfig
{
    public static IServiceCollection AddHandlerConfig(this IServiceCollection services)
    {
        services.AddScoped<IDbSession, DbSession>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        RegisterUseCases(services);
        RegisterRepositories(services);

        Handler.CurrentProvider = services.BuildServiceProvider();
        return services;
    }

    private static void RegisterRepositories(IServiceCollection services) => Assembly
        .GetAssembly(typeof(BaseRepository))!
        .GetTypes()
        .Where(type => type.IsSealed && type.IsSubclassOf(typeof(BaseRepository)))
        .ToList()
        .ForEach(type => services.AddTransient(type.GetInterfaces()
            .ToList()
            .FirstOrDefault(typeInterfaced => typeInterfaced.GetInterfaces().Any())!, type));

    private static void RegisterUseCases(IServiceCollection services) => Assembly
        .GetAssembly(typeof(IUseCase<IRequest, IResponse>))!
        .GetTypes()
        .Where(type =>
            type.IsSealed &&
            type.GetInterfaces().ToList().Any(typeInterfaced =>
                (typeInterfaced.Namespace?.Equals(typeof(IUseCase<IRequest, IResponse>).Namespace) ?? false) &&
                typeInterfaced.Name.Equals(typeof(IUseCase<IRequest, IResponse>).Name)))
        .ToList()
        .ForEach(type => services.AddTransient(type));
}