using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Formality.App.Forms.Models;
using Formality.App.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Forms.Commands
{
    public class CreateFormCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }

    public sealed class CreateFormCommandHandler : IRequestHandler<CreateFormCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateFormCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CreateFormCommand request,
            CancellationToken cancellationToken)
        {
            if (await _context.Forms.AnyAsync(x => x.Name == request.Name, cancellationToken))
            {
                throw new InvalidOperationException($@"Form ""{request.Name}"" already exists.");
            }

            var form = new Form
            {
                Name = request.Name,
            };

            _context.Add(form);

            await _context.SaveChangesAsync(CancellationToken.None);

            return form.Id;
        }
    }

    public sealed class CreateFormCommandValidator : AbstractValidator<UpdateFormCommand>
    {
        public CreateFormCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
        }
    }
}
