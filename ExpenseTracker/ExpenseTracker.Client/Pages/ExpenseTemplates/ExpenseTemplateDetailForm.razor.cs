using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseTemplates;

public partial class ExpenseTemplateDetailForm
{
    [Parameter]
    public ExpenseTemplateDTO? SelectedTemplate { get; set; }
    
    private bool IsEditForm => SelectedTemplate != null;
    
    private ExpenseTemplateFormDTO ExpenseTemplateForm { get; set; } = new();
    
    private List<ExpenseCategoryDTO> Categories { get; set; } = new();
    
    private List<ExpenseTagDTO> Tags { get; set; } = new();
    
    [Inject]
    private IExpenseTemplatesService ExpenseTemplatesService { get; set; } = null!;
    
    [Inject]
    private IExpenseCategoriesService CategoriesService { get; set; } = null!;
    
    [Inject]
    private IExpenseTagsService TagsService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject]
    private ToastService ToastService { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        Categories = (await CategoriesService.GetCategories())?.Data ?? new();
        Tags = (await TagsService.GetTags())?.Data ?? new();

        if (SelectedTemplate != null)
        {
            ExpenseTemplateForm = SelectedTemplate.ToForm();
        }
    }
    
    private bool IsActiveTag(Guid tagId)
    {
        return ExpenseTemplateForm.TagIds.Contains(tagId);
    }
    
    private void TagClicked(Guid tagId)
    {
        var isChecked = ExpenseTemplateForm.TagIds.Contains(tagId);
        if (isChecked)
        {
            ExpenseTemplateForm.TagIds.Remove(tagId);
        }
        else
        {
            ExpenseTemplateForm.TagIds.Add(tagId);
        }
    }
    
    private async Task OnSubmitForm()
    {
        if (IsEditForm)
        {
            await UpdateExpenseTemplate();
        }
        else
        {
            await CreateExpenseTemplate();
        }
    }
    
    private async Task CreateExpenseTemplate()
    {
        var createResult = await ExpenseTemplatesService.CreateTemplate(ExpenseTemplateForm);

        if (createResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense Template successfully created!"
            });
                
            NavigationManager.NavigateTo("/expenses/templates");
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = createResult.ErrorMessage
            });
        }
    }
    
    private async Task UpdateExpenseTemplate()
    {
        var updateResult = await ExpenseTemplatesService.UpdateTemplate(SelectedTemplate!.ExpenseTemplateId, ExpenseTemplateForm);
        
        if (updateResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense Template successfully updated!"
            });
            NavigationManager.NavigateTo("/expenses/templates");
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = updateResult.ErrorMessage
            });
        }
    }
}
