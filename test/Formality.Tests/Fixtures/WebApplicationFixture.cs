using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Formality.Api.Extensions;
using Formality.App.Infrastructure;
using Xunit;

namespace Formality.Tests.Fixtures;

public interface IWebApplicationFixture : IClassFixture<WebApplicationFixture>
{
}

public class WebApplicationFixture : WebApplicationFactory<Formality.Api.Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.AddScoped<AppDbContextSeed, AppDbContextTestSeed>();
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        // TODO: proper clean up
        File.Delete("testdb.sqlite");

        host.MigrateDbContext((App.Infrastructure.AppDbContext _, IServiceProvider services) =>
        {
            var seed = services.GetRequiredService<AppDbContextSeed>();
            seed.SeedAsync().GetAwaiter().GetResult();
        });

        return host;
    }
}
