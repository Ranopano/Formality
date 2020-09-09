using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Formality.App.Common.Extensions;
using Formality.App.Common.Queries;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Models;
using Formality.App.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Forms.Queries
{
    public class SearchFormQuery : SearchQuery<FormListDto[]>
    {
        public FormState? StateId { get; set; }
    }

    public sealed class SearchFormQueryHandler : IRequestHandler<SearchFormQuery, FormListDto[]>
    {
        private readonly IReadOnlyAppDbContext _context;

        private readonly IMapper _mapper;

        public SearchFormQueryHandler(IReadOnlyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormListDto[]> Handle(
            SearchFormQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Forms
                .Where(x => request.StateId == null || x.StateId == request.StateId)
                .WithOrderBy(request)
                .Take(request.MaxResults);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var pattern = $"%{request.Keyword}%";
                query = query.Where(x => EF.Functions.Like(x.Name, pattern));
            }

            var fields = await _mapper
                .ProjectTo<FormListDto>(query)
                .ToArrayAsync(cancellationToken);

            return fields;
        }
    }
}
