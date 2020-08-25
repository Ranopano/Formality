using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;
using Formality.App.Infrastructure;
using System;
using Formality.App.Common.Exceptions;

namespace Formality.App.Forms.Commands
{
    public sealed class EditFormCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;

        public FormFieldDto[] Fields { get; } = Array.Empty<FormFieldDto>();
    }

    public sealed class EditFormCommandHandler : AsyncRequestHandler<EditFormCommand>
    {
        private readonly AppDbContext _context;

        public EditFormCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        protected async override Task Handle(
            EditFormCommand request,
            CancellationToken cancellationToken)
        {
            var form = await _context.Forms
                .Include(x => x.Fields.Where(f => !f.Deleted))
                .ThenInclude(x => x.Values)
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (form == null)
            {
                throw new DomainException($@"Form ""{request.Name}"" doesn't exist yet.");
            }

            foreach (var entity in form.Fields)
            {
                entity.Deleted = true;
            }

            var entities = new List<FormField>(request.Fields.Length);

            foreach (var dto in request.Fields)
            {
                var entity = form.Fields.FirstOrDefault(x => x.Id == dto.Id) ?? new FormField();

                entity.Deleted = false;
                entity.Form = form;
                entity.Label = dto.Label;
                entity.Name = dto.Name;
                entity.Placeholder = dto.Placeholder;
                entity.Type = dto.Type;

                entity.Values.Clear();

                foreach (var dtoValue in dto.Values)
                {
                    var entityValue = new FormFieldValue
                    {
                        Field = entity,
                        Value = dtoValue.Value,
                    };

                    entity.Values.Add(entityValue);
                }

                entities.Add(entity);
            }

            _context.UpdateRange(entities);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    public sealed class UpdateFormCommandValidator : AbstractValidator<EditFormCommand>
    {
        public UpdateFormCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Fields)
                .NotEmpty();

            RuleForEach(x => x.Fields)
                .ChildRules(field =>
                {
                    field.RuleFor(x => x.Name)
                        .NotEmpty()
                        .MaximumLength(2000)
                        .Matches(@"^[\w\d-]+$");

                    field.RuleFor(x => x.Label)
                        .NotEmpty()
                        .MaximumLength(2000);

                    field.RuleFor(x => x.Values)
                        .NotEmpty();
                });
        }
    }
}
