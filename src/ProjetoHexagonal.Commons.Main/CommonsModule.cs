using Autofac;
using ProjetoHexagonal.Commons.Application;
using ProjetoHexagonal.Commons.Application.Autofac;

namespace ProjetoHexagonal.Commons.Main;

public sealed class CommonsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterHandler(builder);
    }

    private static void RegisterHandler(ContainerBuilder builder)
    {
        builder.Register(c => new AutofacExecutionHandler(c.Resolve<ILifetimeScope>()))
            .As<IHandler>()
            .InstancePerLifetimeScope();
    }
}