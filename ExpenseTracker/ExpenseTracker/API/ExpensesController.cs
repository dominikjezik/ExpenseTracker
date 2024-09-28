using ExpenseTracker.Business.Expenses.Commands;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.Expenses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API;

[Route("api/expenses")]
public class ExpensesController(ILogger<ExpensesController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetExpenses(int page = 1, int pageSize = 10)
    {
        try
        {
            var userId = GetCurrentUserId();
            var expenses = await mediator.Send(new GetExpensesQuery(userId, page, pageSize));
            return Ok(expenses);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expenses.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("{expenseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseDTO>> GetExpense(Guid expenseId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var expense = await mediator.Send(new GetExpenseItemByIdQuery(expenseId, userId));

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expense.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] ExpenseFormDTO expenseForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var createdExpense = await mediator.Send(new CreateExpenseCommand(expenseForm, userId));
            return CreatedAtAction(nameof(GetExpense), new { expenseId = createdExpense.ExpenseId }, createdExpense);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating expense");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut("{expenseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateExpense(Guid expenseId, [FromBody] ExpenseFormDTO expenseForm)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingExpense = await mediator.Send(new GetExpenseByIdQuery(expenseId));
            
            if (existingExpense == null)
            {
                return NotFound();
            }
            
            await mediator.Send(new UpdateExpenseCommand(expenseForm, existingExpense, userId));
            
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating expense");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete("{expenseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteExpense(Guid expenseId)
    {
        try
        {
            var userId = GetCurrentUserId();
            var existingExpense = await mediator.Send(new GetExpenseByIdQuery(expenseId));

            if (existingExpense == null)
            {
                return NotFound();
            }

            await mediator.Send(new DeleteExpenseCommand(existingExpense, userId));

            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting expense");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
