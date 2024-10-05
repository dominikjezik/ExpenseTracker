using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseTags;

public partial class Index
{
    private ExpenseTagDetailModal tagDetailModal = default!;
    
    private ConfirmDialog deleteDialog = default!;

    private Result<List<ExpenseTagDTO>> Tags { get; set; } = Result<List<ExpenseTagDTO>>.Loading();

    [Inject] 
    private IExpenseTagsService TagsService { get; set; } = default!;

    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        await FetchTags();
    }

    private async Task FetchTags()
    {
        Tags = await TagsService.GetTags();
    }

    private async Task OnCreateButtonClick()
    {
        await tagDetailModal.ShowAsync();
    }

    private async Task OnRowClick(GridRowEventArgs<ExpenseTagDTO> args)
    {
        await tagDetailModal.ShowAsync(args.Item);
    }

    private async Task DeleteTag(ExpenseTagDTO tag)
    {
        var confirmation = await deleteDialog.ShowAsync(
            title: $"Delete Tag \"{tag.Name}\"",
            message1: "Are you sure you want to delete this tag?",
            confirmDialogOptions: new ConfirmDialogOptions
            {
                YesButtonText = "Delete",
                YesButtonColor = ButtonColor.Danger,
                NoButtonText = "Cancel",
                NoButtonColor = ButtonColor.Secondary
            });
        
        if (confirmation)
        {
            var deleteResult = await TagsService.DeleteTag(tag.TagId);

            if (deleteResult.IsSuccess)
            {
                ToastService.Notify(new() {
                    Type = ToastType.Success,
                    Message = $"Expense Tag \"{tag.Name} \" successfully deleted!"
                });
                
                await FetchTags();
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
