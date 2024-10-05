using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseTemplates;

public partial class Update
{
    [Parameter]
    public Guid TemplateId { get; set; }
    
    private ConfirmDialog deleteDialog = default!;
    
    private Result<ExpenseTemplateDTO> SelectedTemplate { get; set; } = Result<ExpenseTemplateDTO>.Loading();
    
    [Inject]
    private IExpenseTemplatesService ExpenseTemplatesService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        SelectedTemplate = await ExpenseTemplatesService.GetTemplateById(TemplateId);
    }
    
    private async Task DeleteTemplate()
    {
        var confirmation = await deleteDialog.ShowAsync(
            title: $"Delete Template",
            message1: "Are you sure you want to delete this template?",
            confirmDialogOptions: new ConfirmDialogOptions
            {
                YesButtonText = "Delete",
                YesButtonColor = ButtonColor.Danger,
                NoButtonText = "Cancel",
                NoButtonColor = ButtonColor.Secondary
            });
        
        if (confirmation)
        {
            var deleteResult = await ExpenseTemplatesService.DeleteTemplate(TemplateId);
            
            if (deleteResult.IsSuccess)
            {
                ToastService.Notify(new() {
                    Type = ToastType.Success,
                    Message = $"Template successfully deleted!"
                });
                
                NavigationManager.NavigateTo("/expenses/templates");
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
