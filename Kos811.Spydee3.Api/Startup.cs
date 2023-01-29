using Kos811.Spydee3.Services;

namespace Kos811.Spydee3.Api;

public class Startup
{
    //private readonly IConfiguration _configuration;

    //public Startup(IConfiguration configuration) =>_configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();


        services.AddTransient<StubService>();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        if (env.IsDevelopment())
        {
            _ = app.UseSwagger()
                .UseSwaggerUI();
        }
    }
}
