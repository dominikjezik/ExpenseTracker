using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Business.Incomes.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Incomes;

public partial class IncomeDetailForm
{
    [Parameter] 
    public IncomeDTO? SelectedIncome { get; set; }
    
    private bool IsCreateForm => SelectedIncome == null;
    
    private IncomeFormDTO IncomeForm { get; set; } = new();
    
    private List<IncomeCategoryDTO> Categories { get; set; } = new();
    
    [Inject]
    private IIncomesService IncomesService { get; set; } = null!;
    
    [Inject]
    private IIncomeCategoriesService CategoriesService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private ToastService ToastService { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        Categories = (await CategoriesService.GetCategories())?.Data ?? new();

        if (SelectedIncome != null)
        {
            IncomeForm = SelectedIncome.ToForm();
        }
    }

    private async Task OnSubmitForm()
    {
        if (IsCreateForm)
        {
            await CreateIncome();
        }
        else
        {
            await UpdateIncome();
        }
    }

    private async Task CreateIncome()
    {
        var createResult = await IncomesService.CreateIncome(IncomeForm);

        if (createResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Income successfully created!"
            });
                
            NavigationManager.NavigateTo("/incomes");
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = createResult.ErrorMessage
            });
        }
    }

    private async Task UpdateIncome()
    {
        var updateResult = await IncomesService.UpdateIncome(SelectedIncome!.IncomeId, IncomeForm);
        
        if (updateResult.IsSuccess)
        {
            ToastService.Notify(new() {
                Type = ToastType.Success,
                Message = $"Income successfully updated!"
            });
            NavigationManager.NavigateTo("/incomes");
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
