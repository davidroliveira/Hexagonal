using Microsoft.Extensions.DependencyInjection;

namespace Projeto.Mapper;

public static class ServiceMapperConfig
{
    public static IServiceCollection AddMapperConfig(this IServiceCollection services)
    {
        services.AddSingleton(new MapperConfiguration().CreateMapper());
        return services;
    }
}