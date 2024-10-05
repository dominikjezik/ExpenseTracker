using ExpenseTracker.Business.Incomes.Commands;
using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Business.Incomes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.API;

[Authorize]
[Route("api/incomes")]
public class IncomesController(ILogger<IncomesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<IncomeDTO>>> GetIncomes(DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            var userId = GetCurrentUserId();
            var incomes = await mediator.Send(new GetIncomesQuery(userId, fromDate, toDate));
            return Ok(incomes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching incomes.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("{incomeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IncomeDTO>> GetIncome(Guid incomeId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var income = await mediator.Send(new GetIncomeItemByIdQuery(incomeId, userId));

            if (income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching income.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IncomeDTO>> CreateIncome([FromBody] IncomeFormDTO incomeForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdIncome = await mediator.Send(new CreateIncomeCommand(incomeForm, userId));
            return CreatedAtAction(nameof(GetIncome), new { incomeId = createdIncome.IncomeId }, createdIncome);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating income");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{incomeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateIncome(Guid incomeId, [FromBody] IncomeFormDTO incomeForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingIncome = await mediator.Send(new GetIncomeByIdQuery(incomeId));
            
            if (existingIncome == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateIncomeCommand(incomeForm, existingIncome, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating income");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{incomeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteIncome(Guid incomeId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingIncome = await mediator.Send(new GetIncomeByIdQuery(incomeId));
            
            if (existingIncome == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new DeleteIncomeCommand(existingIncome, userId));
            
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting income");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
