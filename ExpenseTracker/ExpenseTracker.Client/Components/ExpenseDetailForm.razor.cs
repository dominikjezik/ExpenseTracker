using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Components;

public partial class ExpenseDetailForm
{
    [Parameter] 
    public ExpenseDTO? SelectedExpense { get; set; }
    
    private bool IsCreateForm => SelectedExpense == null;
    
    private ExpenseFormDTO ExpenseForm { get; set; } = new();
    
    private List<ExpenseCategoryDTO> Categories { get; set; } = new();
    
    private List<ExpenseTagDTO> Tags { get; set; } = new();
    
    [Inject]
    private IExpensesService ExpensesService { get; set; } = null!;
    
    [Inject]
    private IExpenseCategoriesService CategoriesService { get; set; } = null!;
    
    [Inject]
    private IExpenseTagsService TagsService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        Categories = (await CategoriesService.GetCategories())?.Data ?? new();
        Tags = (await TagsService.GetTags())?.Data ?? new();

        if (SelectedExpense != null)
        {
            ExpenseForm = SelectedExpense.ToForm();
        }
    }

    private void TagChecked(Guid tagId, ChangeEventArgs eventArgs)
    {
        if (eventArgs.Value is bool isChecked)
        {
            if (isChecked)
            {
                ExpenseForm.TagIds.Add(tagId);
            }
            else
            {
                ExpenseForm.TagIds.Remove(tagId);
            }
        }
    }

    private async Task SubmitForm()
    {
        if (IsCreateForm)
        {
            var createResult = await ExpensesService.CreateExpense(ExpenseForm);

            if (createResult.IsSuccess)
            {
                NavigationManager.NavigateTo("/expenses");
            }
            else
            {
                // TODO: Handle error
            }

            return;
        }
        
        var updateResult = await ExpensesService.UpdateExpense(SelectedExpense!.ExpenseId, ExpenseForm);
        
        if (updateResult.IsSuccess)
        {
            // TODO: Display success message
        }
        else
        {
            // TODO: Handle error
        }
    }
}
