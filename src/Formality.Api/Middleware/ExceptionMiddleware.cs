using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Formality.App.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Formality.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainException exception)
            {
                await HandleDomainException(context, exception);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unexpected error occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        private async Task HandleDomainException(HttpContext context, DomainException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;

                // TODO: return a proper typed response
                var response = JsonSerializer.Serialize(new
                {
                    context.Response.StatusCode,
                    exception.Message
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsync(response);
            }
        }
    }
}
