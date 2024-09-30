using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Statistics.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IStatisticsService
{
    Task<Result<List<MonthDataItemDTO>>> GetYearStatistics();
    
    Task<Result<List<CategoryExpenseDataItemDTO>>> GetCategoriesExpenses(DateTime? from, DateTime? to);
    
    Task<Result<BalanceDTO>> GetBalance(DateTime? from, DateTime? to);
}
