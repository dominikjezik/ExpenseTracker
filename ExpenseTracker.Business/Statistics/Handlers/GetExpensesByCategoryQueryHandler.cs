using ExpenseTracker.Business.Statistics.DTOs;
using ExpenseTracker.Business.Statistics.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Statistics.Handlers;

public class GetExpensesByCategoryQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetExpensesByCategoryQuery, IEnumerable<CategoryExpenseDataItemDTO>>
{
    public async Task<IEnumerable<CategoryExpenseDataItemDTO>> Handle(GetExpensesByCategoryQuery request, CancellationToken cancellationToken)
    {
        // If "From" or "To" is not provided, set them to the first and last day of the current month
        DateTime from = request.From ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime to = request.To ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
        
        return await context.Expenses
            .Where(e => e.UserId == request.UserId)
            .Where(e => e.CreatedAt >= from && e.CreatedAt <= to)
            .GroupBy(e => e.Category)
            .Select(g => new CategoryExpenseDataItemDTO
            {
                Category = g.Key == null ? string.Empty : g.Key.Name,
                Expense = g.Sum(e => e.Amount)
            })
            .ToListAsync();
    }
}
