using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Formality.Api.Extensions;
using Formality.App.Infrastructure;

namespace Formality.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var env = host.Services.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            host.MigrateDbContext<AppDbContext>((_, services) =>
            {
                var seed = services.GetRequiredService<AppDbContextSeed>();
                seed.SeedAsync().GetAwaiter().GetResult();
            });
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
