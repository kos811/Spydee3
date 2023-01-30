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
        services.AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services.AddTransient<IClickHouseConnection, ClickHouseConnection>(_ => new ClickHouseConnection(_configuration.GetConnectionString("Clickhouse")));

        services.AddTransient<StubService>();
        services.AddTransient<PageDownloader>();
        services.AddTransient<HttpClient>();

        services.AddTransient<PageRepository>();
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
