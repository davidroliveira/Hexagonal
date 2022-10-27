using Microsoft.Extensions.DependencyInjection;

namespace Projeto.Main;

public abstract class Handler
{
    public static IServiceProvider CurrentProvider { private get; set; } = null!;

    public static T Handle<T>() => CurrentProvider.GetService<T>()!;
}