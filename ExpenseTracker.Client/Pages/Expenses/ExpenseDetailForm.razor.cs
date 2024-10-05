using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class ExpenseDetailForm
{
    [Parameter] 
    public ExpenseDTO? SelectedExpense { get; set; }

    [Parameter] 
    public bool IsEditForm { get; set; } = false;

    private ExpenseFormDTO ExpenseForm { get; set; } = new();
    
    private List<ExpenseCategoryDTO> Categories { get; set; } = new();

    private Result<List<ExpenseTagDTO>> Tags { get; set; } = Result<List<ExpenseTagDTO>>.Loading();
    
    [Inject]
    private IExpensesService ExpensesService { get; set; } = null!;
    
    [Inject]
    private IExpenseCategoriesService CategoriesService { get; set; } = null!;
    
    [Inject]
    private IExpenseTagsService TagsService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        Categories = (await CategoriesService.GetCategories())?.Data ?? new();

        if (SelectedExpense == null)
        {
            Tags = await TagsService.GetTagsByCategory(null, includeGeneral: true);
        }
        else
        {
            ExpenseForm = SelectedExpense.ToForm();
            Tags = await TagsService.GetTagsByCategory(ExpenseForm.CategoryId, includeGeneral: true);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedExpense != null)
        {
            ExpenseForm = SelectedExpense.ToForm();
            if (ExpenseForm.CategoryId != null)
            {
                Tags = await TagsService.GetTagsByCategory(ExpenseForm.CategoryId, includeGeneral: true);
            }
        }
    }
    
    private async Task OnCategoryChanged(Guid? categoryId)
    {
        ExpenseForm.CategoryId = categoryId;
        Tags = await TagsService.GetTagsByCategory(ExpenseForm.CategoryId, includeGeneral: true);
    }

    private bool IsActiveTag(Guid tagId)
    {
        return ExpenseForm.TagIds.Contains(tagId);
    }
    
    private void TagClicked(Guid tagId)
    {
        var isChecked = ExpenseForm.TagIds.Contains(tagId);
        if (isChecked)
        {
            ExpenseForm.TagIds.Remove(tagId);
        }
        else
        {
            ExpenseForm.TagIds.Add(tagId);
        }
    }

    private async Task OnSubmitForm()
    {
        if (IsEditForm)
        {
            await UpdateExpense();
        }
        else
        {
            await CreateExpense();
        }
    }

    private async Task CreateExpense()
    {
        var createResult = await ExpensesService.CreateExpense(ExpenseForm);

        if (createResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense successfully created!"
            });
                
            NavigationManager.NavigateTo("/expenses");
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = createResult.ErrorMessage
            });
        }
    }

    private async Task UpdateExpense()
    {
        var updateResult = await ExpensesService.UpdateExpense(SelectedExpense!.ExpenseId, ExpenseForm);
        
        if (updateResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Expense successfully updated!"
            });
            NavigationManager.NavigateTo("/expenses");
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = updateResult.ErrorMessage
            });
        }
    }
    
    public ExpenseFormDTO GetExpenseForm()
    {
        return ExpenseForm;
    }
}
