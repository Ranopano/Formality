using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using FluentValidation.AspNetCore;
using Formality.Api.Middleware;
using Formality.App.Infrastructure;
using Formality.App.Infrastructure.MediatR;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Formality.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Title = configuration["Title"];
        }

        public IConfiguration Configuration { get; }

        private string Title { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(AppDbContext).Assembly;

            services.AddCors();

            services.AddRouting(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });

            services
                .AddControllers(options =>
                {
                    options.ModelBinderProviders
                        .Insert(0, new ModelBinders.SearchQueryModelBinder.Provider());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                .AddFluentValidation(options =>
                {
                    options.AutomaticValidationEnabled = false;
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    options.RegisterValidatorsFromAssembly(assembly, lifetime: ServiceLifetime.Scoped);
                });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Sqlite"), x =>
                {
                    x.MigrationsAssembly(assembly.FullName);
                });
            });

            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);

            services.AddSwaggerGen(x =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                x.SwaggerDoc("v1", new OpenApiInfo { Title = $"{Title} API", Version = "v1" });
                x.DescribeAllParametersInCamelCase();
                x.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<AppDbContextSeed>();
            services.AddScoped<IReadOnlyAppDbContext, AppDbContext>();

            services.AddScoped<TransactionMiddleware>();
            services.AddScoped<ExceptionMiddleware>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors(builder =>
                builder.SetPreflightMaxAge(TimeSpan.FromHours(1))
                    .WithOrigins(Configuration["ClientAppUrl"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Title} API V1");
            });

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
