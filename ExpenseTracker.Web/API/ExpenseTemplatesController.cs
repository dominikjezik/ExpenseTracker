using ExpenseTracker.Business.ExpenseTemplates.Commands;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Business.ExpenseTemplates.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.API;

[Authorize]
[Route("api/expenses/templates")]
public class ExpenseTemplatesController(ILogger<ExpenseTemplatesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExpenseTemplateDTO>>> GetExpenseTemplates([FromQuery] string? organizationName)
    {
        try
        {
            var userId = GetCurrentUserId();
            var expenseTemplates = await mediator.Send(new GetExpenseTemplatesQuery(userId, organizationName));
            return Ok(expenseTemplates);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expense templates.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("{expenseTemplateId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseTemplateDTO>> GetExpenseTemplate(Guid expenseTemplateId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var expenseTemplate = await mediator.Send(new GetExpenseTemplateItemByIdQuery(expenseTemplateId, userId));

            if (expenseTemplate == null)
            {
                return NotFound();
            }

            return Ok(expenseTemplate);
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expense template.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseTemplateDTO>> CreateExpenseTemplate([FromBody] ExpenseTemplateFormDTO expenseTemplateForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdExpenseTemplate = await mediator.Send(new CreateExpenseTemplateCommand(expenseTemplateForm, userId));
            return CreatedAtAction(nameof(GetExpenseTemplate), new { expenseTemplateId = createdExpenseTemplate.ExpenseTemplateId }, createdExpenseTemplate);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating expense template.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{expenseTemplateId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateExpenseTemplate(Guid expenseTemplateId, [FromBody] ExpenseTemplateFormDTO expenseTemplateForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingExpenseTemplate = await mediator.Send(new GetExpenseTemplateByIdQuery(expenseTemplateId));
            
            if (existingExpenseTemplate == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateExpenseTemplateCommand(expenseTemplateForm, existingExpenseTemplate, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating expense template.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{expenseTemplateId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteExpenseTemplate(Guid expenseTemplateId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingExpenseTemplate = await mediator.Send(new GetExpenseTemplateByIdQuery(expenseTemplateId));
            
            if (existingExpenseTemplate == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new DeleteExpenseTemplateCommand(existingExpenseTemplate, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting expense template.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
