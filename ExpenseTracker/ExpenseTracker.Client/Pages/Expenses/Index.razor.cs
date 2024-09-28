using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Index
{
    [Inject] 
    private IExpensesService ExpensesService { get; set; } = default!;
    
    private Result<List<ExpenseDTO>> Expenses { get; set; } = Result<List<ExpenseDTO>>.Loading();
    
    protected override async Task OnInitializedAsync()
    {
        Expenses = await ExpensesService.GetExpenses();
    }
}
