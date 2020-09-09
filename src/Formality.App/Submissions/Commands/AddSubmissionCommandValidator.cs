using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;
using Formality.App.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Submissions.Commands
{
    public sealed class AddSubmissionCommandValidator : AbstractValidator<AddSubmissionCommand>
    {
        private readonly IReadOnlyAppDbContext _context;

        public AddSubmissionCommandValidator(
            IReadOnlyAppDbContext context,
            IMapper mapper,
            IValidator<FormFieldValuesDto> formFieldsDtoValidator)
        {
            _context = context;

            RuleFor(x => x.FormId).NotEmpty();
            RuleFor(x => x.Values).NotEmpty();

            RuleFor(x => x.FormId)
                .MustAsync(AvailableFormExists)
                .WithMessage("Cannot find the available form for this submission.");

            RuleFor(x => x)
                .Transform(mapper.Map<FormFieldValuesDto>)
                .SetValidator(formFieldsDtoValidator);
        }

        private Task<bool> AvailableFormExists(int id, CancellationToken cancellationToken)
        {
            return _context.Forms
                .Where(x => x.Id == id && x.StateId == FormState.Actual)
                .AnyAsync(cancellationToken);
        }
    }
}
