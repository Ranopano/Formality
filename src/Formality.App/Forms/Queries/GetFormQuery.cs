using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Formality.App.Forms.Dto;
using Formality.App.Infrastructure;

namespace Formality.App.Forms.Queries
{
    public sealed class GetFormQuery : IRequest<FormDto>
    {
        public int Id { get; set; }
    }

    public sealed class GetFormQueryHandler : IRequestHandler<GetFormQuery, FormDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetFormQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FormDto> Handle(
            GetFormQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Forms
                .Include(x => x.Fields)
                .ThenInclude(x => x.Values)
                .Where(x => x.Id == request.Id);

            var result = await _mapper
                .ProjectTo<FormDto>(query, null, x => x.Fields)
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
