using System;
using System.Threading;
using System.Threading.Tasks;
using Formality.App.Forms.Commands;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Formality.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Returns all forms that match a provided query.
    /// </summary>
    /// <param name="query">
    ///     A query with searching parameters.
    ///     Usage: /api/forms?{ "keyword": "foo", "maxResults": 10 }
    /// </param>
    /// <param name="cancellationToken">A token to stop the current request.</param>
    [HttpGet]
    public async Task<ActionResult<FormListDto[]>> SearchForm(
        SearchFormQuery? query = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(
            query ?? new SearchFormQuery(),
            cancellationToken);

        return result ?? Array.Empty<FormListDto>();
    }

    /// <summary>
    ///     Returns a form that matches a provided identifier.
    /// </summary>
    /// <param name="id">An identifier of a form.</param>
    /// <param name="cancellationToken">A token to stop the current request.</param>
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

    /// <summary>
    ///     Adds a new form for submissions.
    /// </summary>
    /// <param name="command">A command to add a form.</param>
    /// <param name="cancellationToken">A token to stop the current request.</param>
    [HttpPost]
    public async Task<ActionResult<int>> CreateForm(
        CreateFormCommand command,
        CancellationToken cancellationToken = default)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetForm), new { id }, id);
    }

    /// <summary>
    ///     Updates information about a form and/or its fields.
    /// </summary>
    /// <param name="command">A command to update a form.</param>
    /// <param name="cancellationToken">A token to stop the current request.</param>
    [HttpPut]
    public async Task<ActionResult> EditForm(
        EditFormCommand command,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
