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

        /// <summary>
        ///     Returns all submissions that match a provided query.
        /// </summary>
        /// <param name="query">
        ///     A query with searching parameters.
        ///     Usage: /api/submissions?{ "keyword": "foo", "maxResults": 10 }
        /// </param>
        /// <param name="cancellationToken">A token to stop the current request.</param>
        [HttpGet]
        public async Task<ActionResult<SubmissionListDto[]>> SearchSubmission(
            SearchSubmissionQuery? query = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                query ?? new SearchSubmissionQuery(),
                cancellationToken);

            return result;
        }

        /// <summary>
        ///     Returns a submission that matches a provided identifier.
        /// </summary>
        /// <param name="id">An identifier of a submission.</param>
        /// <param name="cancellationToken">A token to stop the current request.</param>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubmissionDto>> GetSubmission(
            int id,
            CancellationToken cancellationToken = default)
        {
            var submission = await _mediator.Send(
                new GetSubmissionQuery { Id = id },
                cancellationToken);

            if (submission == null)
            {
                return NotFound();
            }

            return Ok(submission);
        }

        /// <summary>
        ///     Adds a new form submission.
        /// </summary>
        /// <param name="command">A command to add a submission.</param>
        /// <param name="cancellationToken">A token to stop the current request.</param>
        [HttpPost]
        public async Task<ActionResult<int>> AddSubmission(
            AddSubmissionCommand command,
            CancellationToken cancellationToken = default)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetSubmission), new { id }, id);
        }
    }
}
