using ClickHouse.Client;
using ClickHouse.Client.ADO;
using Kos811.Spydee3.DataAccess.Repositories;
using Kos811.Spydee3.Services;

namespace Kos811.Spydee3.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        #region MyRegion

        services.AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        #endregion MyRegion

        #region Infra (db, kafka...)

        services.AddTransient<IClickHouseConnection, ClickHouseConnection>(
            _ => new ClickHouseConnection(_configuration.GetConnectionString("Clickhouse")));

        #endregion Infra (db, kafka...)

        #region MyRegion

        services.AddTransient<StubService>();
        services.AddTransient<PageDownloader>();
        services.AddTransient<HttpClient>();

        #endregion Services

        #region Repositories

        services.AddTransient<PageRepository>();
        services.AddTransient<PageParser>();

        #endregion Repositories
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                })
            .UseSwagger()
            .UseSwaggerUI();
    }
}
