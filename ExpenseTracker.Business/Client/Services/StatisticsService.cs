using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Statistics.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class StatisticsService(HttpClient httpClient) : IStatisticsService
{
    public async Task<Result<List<MonthDataItemDTO>>> GetYearStatistics()
    {

        var response = await httpClient.GetAsync("api/statistics/year");
       
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<MonthDataItemDTO>>.Error("Failed to get year statistics.");
        }
        
        var statistics = await response.Content.ReadFromJsonAsync<List<MonthDataItemDTO>>();
        
        return Result<List<MonthDataItemDTO>>.Success(statistics!);
    }

    public async Task<Result<List<CategoryExpenseDataItemDTO>>> GetCategoriesExpenses(DateTime? from, DateTime? to)
    {
        var url = "api/statistics/expenses/categories";
        
        if (from.HasValue && to.HasValue)
        {
            url += $"?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
        }
        
        var response = await httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<CategoryExpenseDataItemDTO>>.Error("Failed to get categories expenses statistics.");
        }
        
        var statistics = await response.Content.ReadFromJsonAsync<List<CategoryExpenseDataItemDTO>>();
        
        return Result<List<CategoryExpenseDataItemDTO>>.Success(statistics!);
    }

    public async Task<Result<BalanceDTO>> GetBalance(DateTime? from, DateTime? to)
    {
        var url = "api/statistics/balance";
        
        if (from.HasValue && to.HasValue)
        {
            url += $"?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}";
        }
        
        var response = await httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<BalanceDTO>.Error("Failed to get expenses income statistics.");
        }
        
        var statistics = await response.Content.ReadFromJsonAsync<BalanceDTO>();
        
        return Result<BalanceDTO>.Success(statistics!);
    }
}
