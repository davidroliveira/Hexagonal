using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.Base;
using Projeto.Persistence;
using System.Reflection;

namespace Projeto.Main;

public static class ServiceHandlerConfig
{
    public static IServiceCollection AddHandlerConfig(this IServiceCollection services)
    {
        Assembly
            .GetAssembly(typeof(BaseUseCase<BaseRequest, BaseResponse>))!
            .GetTypes()
            .Where(type => type.IsSealed &&
                           (type.BaseType?.Namespace?.Equals(typeof(BaseUseCase<BaseRequest, BaseResponse>).Namespace) ?? false) &&
                           (type.BaseType?.Name.Equals(typeof(BaseUseCase<BaseRequest, BaseResponse>).Name) ?? false))
            .ToList()
            .ForEach(type => services.AddTransient(type));

        Assembly
            .GetAssembly(typeof(BaseRepository))!
            .GetTypes()
            .Where(type => type.IsSealed && type.IsSubclassOf(typeof(BaseRepository)))
            .ToList()
            .ForEach(type => services.AddTransient(type.GetInterfaces().ToList().FirstOrDefault(typeInterfaced => typeInterfaced.GetInterfaces().Any())!, type));

        Handler.CurrentProvider = services.BuildServiceProvider();
        return services;
    }
}