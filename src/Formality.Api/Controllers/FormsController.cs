using System;
using System.Threading;
using System.Threading.Tasks;
using Formality.App.Forms.Commands;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Formality.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<FormListDto[]>> SearchForm(
            string? query = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                this.GetQuery<SearchFormQuery>(query),
                cancellationToken);

            return result ?? Array.Empty<FormListDto>();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FormDto>> GetForm(
            int id,
            CancellationToken cancellationToken = default)
        {
            var form = await _mediator.Send(
                new GetFormQuery { Id = id },
                cancellationToken);

            if (form == null)
            {
                return NotFound();
            }

            return Ok(form);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateForm(CreateFormCommandHandler command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetForm), id);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateForm(UpdateFormCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
