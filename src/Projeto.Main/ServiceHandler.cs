using Microsoft.Extensions.DependencyInjection;
using Projeto.Application.UseCases.Modelo;
using Projeto.Application.UseCases.WeatherForecast;
using Projeto.Domain;
using Projeto.Persistence;

namespace Projeto.Main;

public static class ServiceHandler
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        Handler.CurrentProvider = services
                .AddTransient<IModeloRepository, ModeloRepository>()
                .AddTransient<ModeloUseCase>()
                .AddTransient<WeatherForecastUseCase>()
                .BuildServiceProvider();
        return services;
    }
}