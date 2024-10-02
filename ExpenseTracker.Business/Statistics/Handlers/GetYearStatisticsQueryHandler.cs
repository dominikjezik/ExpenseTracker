using ExpenseTracker.Business.Statistics.DTOs;
using ExpenseTracker.Business.Statistics.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Statistics.Handlers;

public class GetYearStatisticsQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetYearStatisticsQuery, IEnumerable<MonthDataItemDTO>>
{
    public async Task<IEnumerable<MonthDataItemDTO>> Handle(GetYearStatisticsQuery request, CancellationToken cancellationToken)
    {
        // --- Used AI to refactor code to this point... (before that it worked but it was a mess...) ---
        var startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month + 1, 1);
        var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
        
        var incomes = await context.Incomes
            .Where(i => i.UserId == request.UserId) 
            .Where(i => i.CreatedAt >= startDate && i.CreatedAt < endDate)
            .GroupBy(i => new { i.CreatedAt.Month })
            .Select(g => 
                new
                {
                    Month = g.Key.Month, 
                    Amount = g.Sum(i => i.Amount)
                })
            .ToListAsync();
        
        var expenses = await context.Expenses
            .Where(e => e.UserId == request.UserId)
            .Where(e => e.CreatedAt >= startDate && e.CreatedAt < endDate)
            .GroupBy(e => new { e.CreatedAt.Month })
            .Select(g => 
                new
                {
                    Month = g.Key.Month, 
                    Amount = g.Sum(e => e.Amount)
                })
            .ToListAsync();
        
        var result = new List<MonthDataItemDTO>();
        
        for (int i = 1; i <= 12; i++)
        {
            var income = incomes.FirstOrDefault(income => income.Month == i);
            var expense = expenses.FirstOrDefault(e => e.Month == i);
            
            result.Add(new MonthDataItemDTO
            {
                Month = i,
                Incomes = income?.Amount ?? 0,
                Expenses = expense?.Amount ?? 0
            });
        }
        
        // --- End of AI refactoring ---
        
        // 1 2 3 4 5 6 7 8 9 10 11 12
        // Let currentMonth be 6
        // 7 8 9 10 11 12 1 2 3 4 5 6
        
        // 1 2 3 4 5 6 7 8 9 10 11 12
        // Let currentMonth be 1
        // 2 3 4 5 6 7 8 9 10 11 12 1
        
        // 1 2 3 4 5 6 7 8 9 10 11 12
        // Let currentMonth be 12
        // 1 2 3 4 5 6 7 8 9 10 11 12
        
        var currentMonth = DateTime.Now.Month;
        
        if (currentMonth == 12)
        {
            return result;
        }
        
        var firstPart = result[currentMonth..];
        var secondPart = result[..currentMonth];
        
        return firstPart.Concat(secondPart);
    }
}
