using ExpenseTracker.Business.ExpenseTags.Commands;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Business.ExpenseTags.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API;

[Route("api/expenses/tags")]
public class ExpenseTagsController(ILogger<ExpensesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExpenseTagDTO>>> GetTags()
    {
        try
        {
            var userId = GetCurrentUserId();
            var expenses = await mediator.Send(new GetExpenseTagsQuery(userId));
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expense tags.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseTagDTO>> CreateTag([FromBody] ExpenseTagFormDTO tagForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdTag = await mediator.Send(new CreateExpenseTagCommand(tagForm, userId));
            return CreatedAtAction(nameof(GetTags), createdTag);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while creating expense tag.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{tagId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateTag(Guid tagId, [FromBody] ExpenseTagFormDTO tagForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingTag = await mediator.Send(new GetExpenseTagByIdQuery(tagId));
            
            if (existingTag == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateExpenseTagCommand(tagForm, existingTag, userId));
            
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating expense tag");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{tagId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteTag(Guid tagId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingTag = await mediator.Send(new GetExpenseTagByIdQuery(tagId));

            if (existingTag == null)
            {
                return NotFound();
            }

            await mediator.Send(new DeleteExpenseTagCommand(existingTag, userId));

            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting expense tag");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
