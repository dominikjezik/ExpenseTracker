using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Incomes.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Incomes;

public partial class Index
{
    private bool FirstLoad { get; set; } = true;
    
    private Result<List<IncomeDTO>> Incomes { get; set; } = Result<List<IncomeDTO>>.Loading();
 
    [Inject] 
    private IIncomesService IncomesService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    private DateTime fromDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    
    private DateTime toDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
    
    protected override async Task OnInitializedAsync()
    {
        // We want expenses from whole last day (without this it would take expenses to 00:00 of toDate)
        var modifiedToDate = toDate.AddDays(1);
        Incomes = await IncomesService.GetIncomes(fromDate, modifiedToDate);
    }
    
    private async Task OnRowClick(GridRowEventArgs<IncomeDTO> args)
    {
        NavigationManager.NavigateTo($"/incomes/{args.Item.IncomeId}");
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
        Incomes = Result<List<IncomeDTO>>.Loading();
        
        // We want expenses from whole last day (without this it would take expenses to 00:00 of toDate)
        var modifiedToDate = toDate.AddDays(1);
        Incomes = await IncomesService.GetIncomes(fromDate, modifiedToDate);
    }
}
