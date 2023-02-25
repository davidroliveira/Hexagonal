using Autofac;
using ProjetoHexagonal.Background;
using ProjetoHexagonal.Commons.Main;
using ProjetoHexagonal.Main;
using Hangfire;
using Hangfire.SqlServer;

namespace ProjetoHexagonal.Web.Blazor;

public sealed class Startup
{
    private readonly IConfiguration config;

    public Startup(IConfiguration config) => this.config = config;

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new CommonsModule());
        builder.RegisterModule(new ProjetoHexagonalModule(config));
    }

    public void ConfigureServices(IServiceCollection services)
    {
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
                SchemaName = "hangfire"
            }));

        services.AddHangfireServer();

        services.AddRazorPages();
        services.AddServerSideBlazor();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            AppPath = null
        });

        RecurringJob.AddOrUpdate<ModeloJob>(
            "ModeloJob",
            job => job.Run(),
            Cron.Hourly,
            TimeZoneInfo.Local);

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
    }
}
