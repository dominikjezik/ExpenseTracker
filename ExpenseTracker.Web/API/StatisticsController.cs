using ExpenseTracker.Business.Statistics.DTOs;
using ExpenseTracker.Business.Statistics.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.API;

[Authorize]
[Route("api/statistics")]
public class StatisticsController(ILogger<StatisticsController> logger, IMediator mediator) : ApplicationApiController
{
    [HttpGet("year")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MonthDataItemDTO>>> GetYearStatistics()
    {
        try
        {
            var userId = GetCurrentUserId();
            var statistics = await mediator.Send(new GetYearStatisticsQuery(userId));
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching year statistics.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("expenses/categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CategoryExpenseDataItemDTO>>> GetExpensesByCategory(DateTime? from, DateTime? to)
    {
        try
        {
            var userId = GetCurrentUserId();
            var statistics = await mediator.Send(new GetExpensesByCategoryQuery(userId, from, to));
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expenses by category statistics.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet("balance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BalanceDTO>> GetBalance(DateTime? from, DateTime? to)
    {
        try
        {
            var userId = GetCurrentUserId();
            var statistics = await mediator.Send(new GetBalanceQuery(userId, from, to));
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was thrown while fetching expenses by category statistics.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
