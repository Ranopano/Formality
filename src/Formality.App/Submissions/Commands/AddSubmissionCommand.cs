using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public AddSubmissionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddSubmissionCommand request, CancellationToken cancellationToken)
        {
            var submission = new Submission { FormId = request.FormId };

            foreach (var valueDto in request.Values)
            {
                var value = new SubmissionValue
                {
                    Submission = submission,
                    FieldId = valueDto.FieldId,
                    Type = valueDto.Type,
                    Value = valueDto.Value,
                };

                submission.Values.Add(value);
            }

            _context.Add(submission);

            await _context.SaveChangesAsync(CancellationToken.None);

            return submission.Id;
        }
    }
}
