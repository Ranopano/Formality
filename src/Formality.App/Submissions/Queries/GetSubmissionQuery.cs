using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Formality.App.Infrastructure;
using Formality.App.Submissions.Dto;

namespace Formality.App.Submissions.Queries;

public sealed class GetSubmissionQuery : IRequest<SubmissionDto?>
{
    public int Id { get; set; }
}

public sealed class GetSubmissionQueryHandler : IRequestHandler<GetSubmissionQuery, SubmissionDto?>
{
    private readonly IReadOnlyAppDbContext _context;

    private readonly IMapper _mapper;

    public GetSubmissionQueryHandler(IReadOnlyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubmissionDto?> Handle(
        GetSubmissionQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Submissions
            .Where(x => x.Id == request.Id);

        var result = await _mapper
            .ProjectTo<SubmissionDto>(query, null, x => x.Values)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
