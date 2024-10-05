using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Incomes.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Incomes;

public partial class Update
{
    [Parameter] 
    public Guid IncomeId { get; set; }
    
    private ConfirmDialog deleteDialog = default!;

    private Result<IncomeDTO> SelectedIncome { get; set; } = Result<IncomeDTO>.Loading();

    [Inject]
    private IIncomesService IncomesService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        SelectedIncome = await IncomesService.GetIncomeById(IncomeId);
    }
    
    private async Task DeleteIncome()
    {
        var confirmation = await deleteDialog.ShowAsync(
            title: $"Delete Income",
            message1: "Are you sure you want to delete this income?",
            confirmDialogOptions: new ConfirmDialogOptions
            {
                YesButtonText = "Delete",
                YesButtonColor = ButtonColor.Danger,
                NoButtonText = "Cancel",
                NoButtonColor = ButtonColor.Secondary
            });
        
        if (confirmation)
        {
            var deleteResult = await IncomesService.DeleteIncome(IncomeId);
            
            if (deleteResult.IsSuccess)
            {
                ToastService.Notify(new() {
                    Type = ToastType.Success,
                    Message = $"Income successfully deleted!"
                });
                
                NavigationManager.NavigateTo("/incomes");
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
