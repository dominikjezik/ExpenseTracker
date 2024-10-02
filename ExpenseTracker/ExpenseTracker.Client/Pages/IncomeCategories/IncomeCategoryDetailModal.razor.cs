using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.IncomeCategories.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.IncomeCategories;

public partial class IncomeCategoryDetailModal
{
    [Parameter] 
    public EventCallback OnSuccess { get; set; }
    
    [Parameter]
    public EventCallback<IncomeCategoryDTO> OnDelete { get; set; }
    
    private IncomeCategoryDTO? SelectedCategory { get; set; }
    
    private bool IsCreateForm => SelectedCategory == null;
    
    private IncomeCategoryFormDTO CategoryForm { get; set; } = new();
    
    private Modal modal = default!;
    
    [Inject]
    private IIncomeCategoriesService CategoriesService { get; set; } = null!;

    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    public async Task ShowAsync(IncomeCategoryDTO? selectedCategory = null)
    {
        if (selectedCategory != null)
        {
            SelectedCategory = selectedCategory;
            CategoryForm = selectedCategory.ToForm();
        }
        else
        {
            SelectedCategory = null;
            CategoryForm = new IncomeCategoryFormDTO();
        }
        
        await modal.ShowAsync();
        StateHasChanged();
    }

    public async Task HideAsync()
    {
        await modal.HideAsync();
    }
    
    private async Task OnSubmitForm()
    {
        if (IsCreateForm)
        {
            await CreateCategory();
        }
        else
        {
            await UpdateCategory();
        }
    }

    private async Task CreateCategory()
    {
        var createResult = await CategoriesService.CreateCategory(CategoryForm);

        if (createResult.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
            await modal.HideAsync();
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Income Category \"{CategoryForm.Name} \" successfully created!"
            });
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = createResult.ErrorMessage
            });
        }
    }

    private async Task UpdateCategory()
    {
        var updateResult = await CategoriesService.UpdateCategory(SelectedCategory!.CategoryId, CategoryForm);

        if (updateResult.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
            await modal.HideAsync();
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Income Category \"{CategoryForm.Name} \" successfully updated!"
            });
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = updateResult.ErrorMessage
            });
        }
    }

    private async Task OnDeleteButtonClick()
    {
        await modal.HideAsync();
        await OnDelete.InvokeAsync(SelectedCategory);
    }
}
