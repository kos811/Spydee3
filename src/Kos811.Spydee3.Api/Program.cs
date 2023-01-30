using Serilog;

namespace Kos811.Spydee3.Api;

public class Program
{
    public static Task Main(string[] args) =>
        CreateHostBuilder(args)
            .Build()
            .RunAsync();

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog(
                (context, services, configuration) =>
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .WriteTo.Console())
            .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
            .ConfigureWebHostDefaults(
                webHostBuilder =>
                    webHostBuilder
                        .UseStartup<Startup>());
}
