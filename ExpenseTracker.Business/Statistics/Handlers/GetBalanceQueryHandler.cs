using ExpenseTracker.Business.Statistics.DTOs;
using ExpenseTracker.Business.Statistics.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Statistics.Handlers;

public class GetBalanceQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetBalanceQuery, BalanceDTO>
{
    public async Task<BalanceDTO> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        // If "From" or "To" is not provided, set them to the first and last day of the current month
        DateTime from = request.From ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime to = request.To ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
        
        // TODO: Implement fetching income
        var incomes = 5000;
        
        var expenses = await context.Expenses
            .Where(e => e.UserId == request.UserId)
            .Where(e => e.CreatedAt >= from && e.CreatedAt <= to)
            .SumAsync(e => e.Amount);
        
        return new BalanceDTO
        {
            Incomes = incomes,
            Expenses = expenses
        };
    }
}
