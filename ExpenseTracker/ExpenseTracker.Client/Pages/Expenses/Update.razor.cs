using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Update
{
    [Parameter] 
    public Guid ExpenseId { get; set; }

    private Result<ExpenseDTO> SelectedExpense { get; set; } = Result<ExpenseDTO>.Loading();

    [Inject]
    private IExpensesService ExpensesService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        SelectedExpense = await ExpensesService.GetExpenseById(ExpenseId);
    }

    private async Task SubmitDelete()
    {
        var response = await ExpensesService.DeleteExpense(ExpenseId);
        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("/expenses");
        }
        else
        {
            // Show error message
        }
    }
}
