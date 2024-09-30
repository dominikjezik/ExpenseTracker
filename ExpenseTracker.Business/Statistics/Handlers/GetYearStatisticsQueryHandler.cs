using ExpenseTracker.Business.Statistics.DTOs;
using ExpenseTracker.Business.Statistics.Queries;
using ExpenseTracker.Data.DbContext;
using MediatR;

namespace ExpenseTracker.Business.Statistics.Handlers;

public class GetYearStatisticsQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetYearStatisticsQuery, IEnumerable<MonthDataItemDTO>>
{
    public async Task<IEnumerable<MonthDataItemDTO>> Handle(GetYearStatisticsQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement the GetYearStatisticsQueryHandler
        return new List<MonthDataItemDTO>
        {
            new() { Month = 10, Incomes = 2000, Expenses = 1400 },
            new() { Month = 11, Incomes = 2100, Expenses = 1500 },
            new() { Month = 12, Incomes = 2200, Expenses = 1600 },
            new() { Month = 1, Incomes = 1000, Expenses = 500 },
            new() { Month = 2, Incomes = 1200, Expenses = 600 },
            new() { Month = 3, Incomes = 1300, Expenses = 700 },
            new() { Month = 4, Incomes = 1400, Expenses = 800 },
            new() { Month = 5, Incomes = 1500, Expenses = 900 },
            new() { Month = 6, Incomes = 1600, Expenses = 1000 },
            new() { Month = 7, Incomes = 1700, Expenses = 1100 },
            new() { Month = 8, Incomes = 1800, Expenses = 1200 },
            new() { Month = 9, Incomes = 1900, Expenses = 1300 }
        };
    }
}
