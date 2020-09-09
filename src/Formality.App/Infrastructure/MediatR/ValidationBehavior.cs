using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Formality.App.Infrastructure.MediatR
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var tasks = _validators.Select(x => x.ValidateAsync(request, cancellationToken));
            var results = await Task.WhenAll(tasks);
            var errors = results.SelectMany(x => x.Errors);

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
    }
}
