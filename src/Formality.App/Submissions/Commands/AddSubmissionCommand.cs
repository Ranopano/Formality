using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Formality.App.Common.Exceptions;
using Formality.App.Forms.Models;
using Formality.App.Forms.Queries;
using Formality.App.Infrastructure;
using Formality.App.Submissions.Dto;
using Formality.App.Submissions.Models;
using MediatR;

namespace Formality.App.Submissions.Commands
{
    public sealed class AddSubmissionCommand : IRequest<int>
    {
        public int FormId { get; set; }

        public IEnumerable<SubmissionValueDto> Values { get; set; } = Enumerable.Empty<SubmissionValueDto>();
    }

    public sealed class AddSubmissionCommandHandler : IRequestHandler<AddSubmissionCommand, int>
    {
        private readonly AppDbContext _context;

        private readonly IMediator _mediator;

        public AddSubmissionCommandHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(
            AddSubmissionCommand request,
            CancellationToken cancellationToken)
        {
            var form = await _mediator.Send(
                new GetFormQuery { Id = request.FormId  },
                cancellationToken);

            if (form == null)
            {
                throw new DomainException(
                    $@"Cannot find a form for this submission.");
            }

            var submission = new Submission();

            foreach (var field in form.Fields)
            {
                var dto = request.Values.FirstOrDefault(x => x.FieldId == field.Id);

                if (dto == null)
                {
                    // TODO: only if the field has required rule
                    throw new InvalidOperationException(
                        $@"This submission doesn't have field ""{field.Name}"".");
                }

                var entity = new SubmissionValue
                {
                    Submission = submission,
                    Field = new FormField { Id = dto.FieldId },
                    Type = field.Type,
                    Value = dto.Value,
                };

                submission.Values.Add(entity);
            }

            _context.Add(submission);

            await _context.SaveChangesAsync(CancellationToken.None);

            return submission.Id;
        }
    }

    public sealed class AddSubmissionCommandValidator : AbstractValidator<AddSubmissionCommand>
    {
        public AddSubmissionCommandValidator()
        {
            RuleFor(x => x.Values).NotEmpty();
            RuleForEach(x => x.Values)
                .ChildRules(value =>
                {
                    value.RuleFor(x => x.Value)
                        .NotEmpty()
                        .MaximumLength(2000);
                });
        }
    }
}
