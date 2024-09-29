using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Update
{
    [Parameter] 
    public Guid ExpenseId { get; set; }
    
    private ConfirmDialog deleteDialog = default!;

    private Result<ExpenseDTO> SelectedExpense { get; set; } = Result<ExpenseDTO>.Loading();

    [Inject]
    private IExpensesService ExpensesService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        SelectedExpense = await ExpensesService.GetExpenseById(ExpenseId);
    }
    
    private async Task DeleteExpense()
    {
        var confirmation = await deleteDialog.ShowAsync(
            title: $"Delete Expense",
            message1: "Are you sure you want to delete this expense?",
            confirmDialogOptions: new ConfirmDialogOptions
            {
                YesButtonText = "Delete",
                YesButtonColor = ButtonColor.Danger,
                NoButtonText = "Cancel",
                NoButtonColor = ButtonColor.Secondary
            });
        
        if (confirmation)
        {
            var deleteResult = await ExpensesService.DeleteExpense(ExpenseId);
            
            if (deleteResult.IsSuccess)
            {
                ToastService.Notify(new() {
                    Type = ToastType.Success,
                    Message = $"Expense successfully deleted!"
                });
                
                NavigationManager.NavigateTo("/expenses");
            }
            else
            {
                ToastService.Notify(new() {
                    Type = ToastType.Danger,
                    Message = deleteResult.ErrorMessage
                });
            }
        }
    }
}
