using Autofac.Extensions.DependencyInjection;
using ProjetoHexagonal.Commons.Configurations;
using NLog;
using NLog.Web;

namespace ProjetoHexagonal.Web.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        try
        {
            CreateHostBuilder(args).Build().Run();
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

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureAppConfiguration(config =>
            {
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

                webBuilder.ConfigureAppConfiguration(config =>
                {
                    config.ReplaceDefaultJsonByEnvironmentJson();
                });
            })
            .UseNLog();
}