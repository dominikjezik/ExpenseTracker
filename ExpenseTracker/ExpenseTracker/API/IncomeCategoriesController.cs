using ExpenseTracker.Business.IncomeCategories.Commands;
using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Business.IncomeCategories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API;

[Route("api/incomes/categories")]
public class IncomeCategoriesController(ILogger<IncomeCategoriesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<IncomeCategoryDTO>>> GetCategories()
    {
        try
        {
            var userId = GetCurrentUserId();
            var categories = await mediator.Send(new GetIncomeCategoriesQuery(userId));
            return Ok(categories);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching income categories.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IncomeCategoryDTO>> CreateCategory([FromBody] IncomeCategoryFormDTO categoryForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdCategory = await mediator.Send(new CreateIncomeCategoryCommand(categoryForm, userId));
            return CreatedAtAction(nameof(GetCategories), createdCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while creating income category.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateCategory(Guid categoryId, [FromBody] IncomeCategoryFormDTO categoryForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingCategory = await mediator.Send(new GetIncomeCategoryByIdQuery(categoryId));
            
            if (existingCategory == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateIncomeCategoryCommand(categoryForm, existingCategory, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating income category");
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
            var existingCategory = await mediator.Send(new GetIncomeCategoryByIdQuery(categoryId));
            
            if (existingCategory == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new DeleteIncomeCategoryCommand(existingCategory, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting income category");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}