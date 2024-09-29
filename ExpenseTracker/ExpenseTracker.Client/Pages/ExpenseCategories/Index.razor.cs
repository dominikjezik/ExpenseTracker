using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseCategories;

public partial class Index
{
    private ExpenseCategoryDetailModal createCategoryModal = default!;
    
    private ConfirmDialog deleteDialog = default!;

    private Result<List<ExpenseCategoryDTO>> Categories { get; set; } = Result<List<ExpenseCategoryDTO>>.Loading();

    [Inject] 
    private IExpenseCategoriesService CategoriesService { get; set; } = default!;

    [Inject] 
    private ToastService ToastService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await FetchCategories();
    }

    private async Task FetchCategories()
    {
        Categories = await CategoriesService.GetCategories();
    }

    private async Task OnCreateButtonClick()
    {
        await createCategoryModal.ShowAsync();
    }

    private async Task OnRowClick(GridRowEventArgs<ExpenseCategoryDTO> args)
    {
        await createCategoryModal.ShowAsync(args.Item);
    }

    private async Task DeleteCategory(ExpenseCategoryDTO category)
    {
        var confirmation = await deleteDialog.ShowAsync(
            title: $"Delete Category \"{category.Name}\"",
            message1: "Are you sure you want to delete this category?",
            confirmDialogOptions: new ConfirmDialogOptions
            {
                YesButtonText = "Delete",
                YesButtonColor = ButtonColor.Danger,
                NoButtonText = "Cancel",
                NoButtonColor = ButtonColor.Secondary
            });
        
        if (confirmation)
        {
            var deleteResult = await CategoriesService.DeleteCategory(category.CategoryId);

            if (deleteResult.IsSuccess)
            {
                ToastService.Notify(new() {
                    Type = ToastType.Success,
                    Message = $"Expense Category \"{category.Name} \" successfully deleted!"
                });
                
                await FetchCategories();
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
