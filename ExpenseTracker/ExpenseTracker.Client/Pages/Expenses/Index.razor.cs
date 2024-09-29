using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Index
{
    private Result<List<ExpenseDTO>> Expenses { get; set; } = Result<List<ExpenseDTO>>.Loading();
 
    [Inject] 
    private IExpensesService ExpensesService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        Expenses = await ExpensesService.GetExpenses();
    }
    
    private async Task OnRowClick(GridRowEventArgs<ExpenseDTO> args)
    {
        NavigationManager.NavigateTo($"/expenses/{args.Item.ExpenseId}");
    }
}
