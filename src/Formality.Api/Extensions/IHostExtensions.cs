using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Formality.Api.Extensions;

public static class IHostExtensions
{
    public static IHost MigrateDbContext<TContext>(
        this IHost host,
        Action<TContext, IServiceProvider>? action = null)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();

        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<TContext>();

        context.Database.Migrate();

        action?.Invoke(context, provider);

        return host;
    }
}
