using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Formality.App.Infrastructure;
using Formality.App.Submissions.Dto;
using Formality.App.Common.Extensions;
using Formality.App.Common.Queries;

namespace Formality.App.Submissions.Queries;

public sealed class SearchSubmissionQuery : SearchQuery<SubmissionListDto[]>
{
}

public sealed class SearchSubmissionQueryHandler
    : IRequestHandler<SearchSubmissionQuery, SubmissionListDto[]>
{
    private readonly IReadOnlyAppDbContext _context;

    private readonly IMapper _mapper;

    public SearchSubmissionQueryHandler(IReadOnlyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SubmissionListDto[]> Handle(
        SearchSubmissionQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Submissions
            .Include(x => x.Values)
            .WithOrderBy(request)
            .Take(request.MaxResults);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var pattern = $"%{request.Keyword}%";
            query = query.Where(x => x.Values
                .Any(v => EF.Functions.Like(v.Value, pattern)));
        }

        var submissions = await _mapper
            .ProjectTo<SubmissionListDto>(query, null)
            .ToArrayAsync(cancellationToken);

        return submissions;
    }
}
