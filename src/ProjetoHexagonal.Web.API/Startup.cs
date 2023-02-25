using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProjetoHexagonal.Commons.Hangfire.Dashboard.Filters;
using ProjetoHexagonal.Commons.Main;
using ProjetoHexagonal.Background;
using ProjetoHexagonal.Main;
using Hangfire;
using Hangfire.SqlServer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ProjetoHexagonal.Web.API;

public sealed class Startup
{
    private readonly IConfiguration config;

    public Startup(IConfiguration config) => this.config = config;

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new CommonsModule(config));
        builder.RegisterModule(new GestorERPModule(config));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddSqlServer(connectionString: config.GetConnectionString("Default")!, name: "Instância do SQL Server");

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        services.AddHangfire(hangfireConfig => hangfireConfig
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseNLogLogProvider()
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(config.GetConnectionString("Hangfire"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                DisableGlobalLocks = true,
                QueuePollInterval = TimeSpan.Zero,
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UsePageLocksOnDequeue = true,
                UseRecommendedIsolationLevel = true,
                SchemaName = "ProjetoHexagonal"
            }));

        services.AddHangfireServer();
    }

    public void Configure(IApplicationBuilder app)
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseHealthChecksUI(options => { options.UIPath = "/healthUI"; });

        GlobalConfiguration.Configuration.UseActivator(new AutofacJobActivator(app.ApplicationServices.GetAutofacRoot()));

        app.UseHangfireDashboard(string.Empty, new DashboardOptions
        {
            Authorization = new[] { new PassthroughAuthorizationFilter() },
            AppPath = null
        });

        RecurringJob.AddOrUpdate<ModeloJob>(
            "ModeloJob",
            job => job.Run(),
            Cron.Hourly,
            TimeZoneInfo.Local);
    }
}
