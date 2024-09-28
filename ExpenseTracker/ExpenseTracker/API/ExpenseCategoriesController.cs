using ExpenseTracker.Business.ExpenseCategories.Commands;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseCategories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API;

[Route("api/expenses/categories")]
public class ExpenseCategoriesController(ILogger<ExpensesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExpenseCategoryDTO>>> GetCategories()
    {
        try
        {
            var userId = GetCurrentUserId();
            var categories = await mediator.Send(new GetExpenseCategoriesQuery(userId));
            return Ok(categories);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expense categories.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseCategoryDTO>> CreateCategory([FromBody] ExpenseCategoryFormDTO categoryForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdCategory = await mediator.Send(new CreateExpenseCategoryCommand(categoryForm, userId));
            return CreatedAtAction(nameof(GetCategories), createdCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while creating expense category.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateCategory(Guid categoryId, [FromBody] ExpenseCategoryFormDTO categoryForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingCategory = await mediator.Send(new GetExpenseCategoryByIdQuery(categoryId));
            
            if (existingCategory == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateExpenseCategoryCommand(categoryForm, existingCategory, userId));
            
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating expense category");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteCategory(Guid categoryId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingCategory = await mediator.Send(new GetExpenseCategoryByIdQuery(categoryId));

            if (existingCategory == null)
            {
                return NotFound();
            }

            await mediator.Send(new DeleteExpenseCategoryCommand(existingCategory, userId));

            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting expense category");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
