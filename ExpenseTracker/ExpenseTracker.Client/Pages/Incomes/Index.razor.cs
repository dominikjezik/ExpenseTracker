using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Incomes.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Incomes;

public partial class Index
{
    private Result<List<IncomeDTO>> Incomes { get; set; } = Result<List<IncomeDTO>>.Loading();
 
    [Inject] 
    private IIncomesService IncomesService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        Incomes = await IncomesService.GetIncomes();
    }
    
    private async Task OnRowClick(GridRowEventArgs<IncomeDTO> args)
    {
        NavigationManager.NavigateTo($"/incomes/{args.Item.IncomeId}");
    }
}
