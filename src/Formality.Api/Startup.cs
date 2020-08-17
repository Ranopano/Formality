using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Formality.App.Infrastructure;

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

            services
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    // TODO: use validation behaviour
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    options.RegisterValidatorsFromAssembly(assembly);
                });

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Sqlite"), o =>
                {
                    o.MigrationsAssembly(assembly.FullName);
                });
            });

            services.AddScoped<AppDbContextSeed>();

            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{Title} API", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            var origins = Configuration["ClientAppUrl"];

            app.UseRouting();
            app.UseCors(builder =>
                builder.SetPreflightMaxAge(TimeSpan.FromHours(1))
                    .WithOrigins(Configuration["ClientAppUrl"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Title} API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
