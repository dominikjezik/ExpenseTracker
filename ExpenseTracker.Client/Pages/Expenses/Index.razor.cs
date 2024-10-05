using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Index
{
    private bool FirstLoad { get; set; } = true;
    
    private Result<List<ExpenseDTO>> Expenses { get; set; } = Result<List<ExpenseDTO>>.Loading();
 
    [Inject] 
    private IExpensesService ExpensesService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    private DateTime fromDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    
    private DateTime toDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

    public int GridPageSize { get; set; } = 10;
    
    protected override async Task OnInitializedAsync()
    {
        // We want expenses from whole last day (without this it would take expenses to 00:00 of toDate)
        var modifiedToDate = toDate.AddDays(1);
        Expenses = await ExpensesService.GetExpenses(fromDate, modifiedToDate);
    }
    
    private async Task OnRowClick(GridRowEventArgs<ExpenseDTO> args)
    {
        NavigationManager.NavigateTo($"/expenses/{args.Item.ExpenseId}");
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
        Expenses = Result<List<ExpenseDTO>>.Loading();
        
        // We want expenses from whole last day (without this it would take expenses to 00:00 of toDate)
        var modifiedToDate = toDate.AddDays(1);
        Expenses = await ExpensesService.GetExpenses(fromDate, modifiedToDate);
    }
    
    private void OnGridSettingsChanged(GridSettings gridSettings)
    {
        GridPageSize = gridSettings.PageSize;
    }
}
