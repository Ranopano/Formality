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

namespace Formality.App.Forms.Commands
{
    public sealed class UpdateFormCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;

        public FormFieldDto[] Fields { get; } = Array.Empty<FormFieldDto>();
    }

    public sealed class UpdateFormCommandHandler : AsyncRequestHandler<UpdateFormCommand>
    {
        private readonly AppDbContext _context;

        public UpdateFormCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        protected async override Task Handle(
            UpdateFormCommand request,
            CancellationToken cancellationToken)
        {
            var form = await _context.Forms
                .Include(x => x.Fields.Where(f => !f.Deleted))
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (form == null)
            {
                throw new InvalidOperationException($@"Form ""{request.Name}"" doesn't exist yet.");
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

                // TODO: save field values

                entities.Add(entity);
            }

            _context.UpdateRange(entities);

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    public sealed class UpdateFormCommandValidator : AbstractValidator<UpdateFormCommand>
    {
        public UpdateFormCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Fields).NotEmpty();
        }
    }
}
