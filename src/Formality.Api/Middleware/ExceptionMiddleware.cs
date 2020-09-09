using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Formality.App.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Formality.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionMiddleware(
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment,
            ProblemDetailsFactory problemDetailsFactory)
        {
            _logger = logger;
            _environment = environment;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HandleException(
                    context,
                    title: "A domain error occurred.",
                    detail: ex.Message,
                    type: "domain");
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                _logger.LogError(ex, ex.Message);

                await HandleException(context, detail: _environment.IsProduction() ? null : ex.Message);
            }
        }

        private async Task HandleException(
            HttpContext context,
            string? title = null,
            string? detail = null,
            string? type = null)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            var details = _problemDetailsFactory.CreateProblemDetails(
                httpContext: context,
                statusCode: context.Response.StatusCode,
                title: title ?? "An unexpected error occurred.",
                type: type,
                detail: detail);

            await SendProblemDetails(context, details);
        }

        private async Task HandleValidationException(HttpContext context, ValidationException ex)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            var modelState = new ModelStateDictionary();
            var validationResult = new ValidationResult(ex.Errors);

            validationResult.AddToModelState(modelState, string.Empty);

            var details = _problemDetailsFactory.CreateValidationProblemDetails(
                httpContext: context,
                statusCode: context.Response.StatusCode,
                title: "Validation failed.",
                type: "validation",
                modelStateDictionary: modelState);

            await SendProblemDetails(context, details);
        }

        private Task SendProblemDetails(HttpContext context, ProblemDetails details)
        {
            var actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());
            var result = new ObjectResult(details) { StatusCode = details.Status };
            return result.ExecuteResultAsync(actionContext);
        }
    }
}
