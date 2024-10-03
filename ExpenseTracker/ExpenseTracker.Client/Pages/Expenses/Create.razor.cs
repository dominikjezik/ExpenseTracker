using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Expenses.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Create
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string? ReceiptCode { get; set; }
    
    private ExpenseDTO? LoadedExpense { get; set; }
    
    [Inject]
    private IReceiptsService ReceiptService { get; set; } = null!;
    
    [Inject]
    private IExpenseTemplatesService ExpenseTemplateService { get; set; } = null!;
    
    [Inject] 
    private ToastService ToastService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrWhiteSpace(ReceiptCode))
        {
            await FetchReceipt();
        }
    }

    private async Task FetchReceipt()
    {
        // Load receipt details by code
        var result = await ReceiptService.GetReceiptByCode(ReceiptCode!);
        
        if (result.IsSuccess)
        {
            var loadedExpense = new ExpenseDTO
            {
                CreatedAt = result.Data.CreateDate ?? DateTime.Now,
                Amount = result.Data.TotalPrice,
                Description = result.Data.Items.Count == 1 ? result.Data.Items[0].Name : string.Empty
            };
            
            // Load expense template
            if (!string.IsNullOrEmpty(result.Data.OrganizationName))
            {
                var templatesResult = await ExpenseTemplateService.GetTemplatesByOrganizationName(result.Data.OrganizationName);
                
                if (templatesResult.IsSuccess)
                {
                    if (templatesResult.Data.Count == 0)
                    {
                        ToastService.Notify(new() {
                            Type = ToastType.Info,
                            Message = "No template found for this organization."
                        });
                    }
                    else
                    {
                        loadedExpense.Category = templatesResult.Data[0].Category;
                        loadedExpense.Tags = templatesResult.Data[0].Tags;
                    }
                }
                else
                {
                    ToastService.Notify(new() {
                        Type = ToastType.Danger,
                        Message = templatesResult.ErrorMessage
                    });
                }
            }
            
            LoadedExpense = loadedExpense;
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = result.ErrorMessage
            });
        }
    }
}
