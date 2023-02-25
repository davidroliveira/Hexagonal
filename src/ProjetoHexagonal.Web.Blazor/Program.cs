using Autofac;
using Autofac.Extensions.DependencyInjection;
using NLog;
using NLog.Web;

namespace ProjetoHexagonal.Web.Blazor;

public static class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);
            var app = builder.Build();
            startup.Configure(app, app.Environment);
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}

