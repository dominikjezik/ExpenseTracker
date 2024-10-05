using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseTags;

public partial class ExpenseTagDetailModal
{
    [Parameter] 
    public EventCallback OnSuccess { get; set; }
    
    [Parameter]
    public EventCallback<ExpenseTagDTO> OnDelete { get; set; }
    
    private ExpenseTagDTO? SelectedTag { get; set; }
    
    private bool IsCreateForm => SelectedTag == null;
    
    private ExpenseTagFormDTO TagForm { get; set; } = new();
    
    private List<ExpenseCategoryDTO> Categories { get; set; } = new();
    
    private Modal modal = default!;
    
    [Inject]
    private IExpenseTagsService TagsService { get; set; } = null!;
    
    [Inject]
    private IExpenseCategoriesService CategoriesService { get; set; } = null!;

    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        Categories = (await CategoriesService.GetCategories())?.Data ?? new();
    }
    
    public async Task ShowAsync(ExpenseTagDTO? selectedTag = null)
    {
        if (selectedTag != null)
        {
            SelectedTag = selectedTag;
            TagForm = selectedTag.ToForm();
        }
        else
        {
            SelectedTag = null;
            TagForm = new ExpenseTagFormDTO();
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
            await CreateTag();
        }
        else
        {
            await UpdateTag();
        }
    }

    private async Task CreateTag()
    {
        Console.WriteLine(TagForm.CategoryId);
        
        var createResult = await TagsService.CreateTag(TagForm);

        if (createResult.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
            await modal.HideAsync();
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense Tag \"{TagForm.Name} \" successfully created!"
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

    private async Task UpdateTag()
    {
        Console.WriteLine(TagForm.CategoryId);

        var updateResult = await TagsService.UpdateTag(SelectedTag!.TagId, TagForm);

        if (updateResult.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
            await modal.HideAsync();
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense Tag \"{TagForm.Name} \" successfully updated!"
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
        await OnDelete.InvokeAsync(SelectedTag);
    }
}
