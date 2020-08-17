using System;
using System.Threading;
using System.Threading.Tasks;
using Formality.App.Submissions.Commands;
using Formality.App.Submissions.Dto;
using Formality.App.Submissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Formality.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubmissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<SubmissionListDto[]>> SearchSubmission(
            string? query = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                this.GetQuery<SearchSubmissionQuery>(query),
                cancellationToken);

            return result;
        }

        [HttpGet("{id:int}")]
        public Task<ActionResult<SubmissionListDto>> GetSubmission(
            int id,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddSubmission(AddSubmissionCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSubmission), id);
        }
    }
}
