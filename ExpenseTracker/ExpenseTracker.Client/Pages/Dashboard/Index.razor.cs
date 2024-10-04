using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Statistics.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Dashboard;

public partial class Index
{
    private bool FirstLoad { get; set; } = true;
    
    private Result<List<MonthDataItemDTO>> YearData { get; set; } = Result<List<MonthDataItemDTO>>.Loading();
    
    private Result<List<CategoryExpenseDataItemDTO>> CategoriesExpensesData { get; set; } = Result<List<CategoryExpenseDataItemDTO>>.Loading();
    
    private Result<BalanceDTO> BalanceData { get; set; } = Result<BalanceDTO>.Loading();
    
    private DateTime fromDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    
    private DateTime toDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
    
    [Inject]
    private IStatisticsService StatisticsService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        // We want expenses from whole last day (without this it would take expenses to 00:00 of toDate)
        var modifiedToDate = toDate.AddDays(1);
        
        var yearDataTask = StatisticsService.GetYearStatistics();
        var categoriesExpensesDataTask = StatisticsService.GetCategoriesExpenses(fromDate, modifiedToDate);
        var balanceDataTask = StatisticsService.GetBalance(fromDate, toDate);
        
        await Task.WhenAll(yearDataTask, categoriesExpensesDataTask, balanceDataTask);
        
        YearData = yearDataTask.Result;
        CategoriesExpensesData = categoriesExpensesDataTask.Result;
        BalanceData = balanceDataTask.Result;
    }
    private async Task OnFromDateChanged(DateTime date)
    {
        fromDate = date;
        await TimeIntervalChanged();
    }
    
    private async Task OnToDateChanged(DateTime date)
    {
        toDate = date;
        await TimeIntervalChanged();
    }
    
    private async Task TimeIntervalChanged()
    {
        FirstLoad = false;
        
        CategoriesExpensesData = Result<List<CategoryExpenseDataItemDTO>>.Loading();
        BalanceData = Result<BalanceDTO>.Loading();
        
        var modifiedToDate = toDate.AddDays(1);
        
        CategoriesExpensesData = await StatisticsService.GetCategoriesExpenses(fromDate, modifiedToDate);
        BalanceData = await StatisticsService.GetBalance(fromDate, toDate);
    }
}
