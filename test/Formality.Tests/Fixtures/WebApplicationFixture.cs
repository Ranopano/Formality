using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Formality.Api.Extensions;
using Formality.App.Infrastructure;
using Xunit;

namespace Formality.Tests.Fixtures
{
    public interface IWebApplicationFixture : IClassFixture<WebApplicationFixture>
    {
    }

    public class WebApplicationFixture : WebApplicationFactory<Formality.Api.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Testing");
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            // TODO: get filename from the config
            File.Delete("db-test.sqlite");

            host.MigrateDbContext((App.Infrastructure.AppDbContext _, IServiceProvider services) =>
            {
                var seed = services.GetRequiredService<AppDbContextSeed>();
                seed.SeedAsync().GetAwaiter().GetResult();
            });

            return host;
        }
    }
}
