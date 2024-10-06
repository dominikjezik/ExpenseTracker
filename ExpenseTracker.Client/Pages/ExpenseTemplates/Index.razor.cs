using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.ExpenseTemplates;

public partial class Index
{
    private Result<List<ExpenseTemplateDTO>> ExpenseTemplates { get; set; } = Result<List<ExpenseTemplateDTO>>.Loading();
 
    [Inject] 
    private IExpenseTemplatesService ExpenseTemplatesService { get; set; } = default!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        ExpenseTemplates = await ExpenseTemplatesService.GetTemplates();
    }
    
    private void OnRowClick(GridRowEventArgs<ExpenseTemplateDTO> args)
    {
        NavigationManager.NavigateTo($"/expenses/templates/{args.Item.ExpenseTemplateId}");
    }
}
