using BlazorBootstrap;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.DTOs;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.Client.Pages.Expenses;

public partial class Create
{
    [Parameter]
    [SupplyParameterFromQuery]
    public string? ReceiptCode { get; set; }

    private Result<ReceiptDTO>? ReceiptResult { get; set; }
    
    private Result<List<ExpenseTemplateDTO>>? TemplatesResult { get; set; }

    private Result<ExpenseTemplateDTO>? CreateTemplateResult { get; set; }
    
    private ExpenseDTO? LoadedExpense { get; set; }

    private ExpenseDetailForm expenseDetailForm = default!;
    
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
        ReceiptResult = Result<ReceiptDTO>.Loading();
        
        // Load receipt details by code
        ReceiptResult = await ReceiptService.GetReceiptByCode(ReceiptCode!);
        
        if (ReceiptResult.IsSuccess)
        {
            var loadedExpense = new ExpenseDTO
            {
                CreatedAt = ReceiptResult.Data.CreateDate ?? DateTime.Now,
                Amount = ReceiptResult.Data.TotalPrice,
                Description = ReceiptResult.Data.Items.Count == 1 ? ReceiptResult.Data.Items[0].Name : string.Empty
            };
            
            // Load expense template
            if (!string.IsNullOrEmpty(ReceiptResult.Data.OrganizationName))
            {
                TemplatesResult = await ExpenseTemplateService.GetTemplatesByOrganizationName(ReceiptResult.Data.OrganizationName);
                
                if (TemplatesResult.IsSuccess)
                {
                    if (TemplatesResult.Data.Count == 0)
                    {
                        ToastService.Notify(new() {
                            Type = ToastType.Info,
                            Message = "No template found for this organization."
                        });
                    }
                    else
                    {
                        loadedExpense.Category = TemplatesResult.Data[0].Category;
                        loadedExpense.Tags = TemplatesResult.Data[0].Tags;
                    }
                }
                else
                {
                    ToastService.Notify(new() {
                        Type = ToastType.Danger,
                        Message = TemplatesResult.ErrorMessage
                    });
                }
            }
            
            await expenseDetailForm.SetExpense(loadedExpense);
        }
        else
        {
            ToastService.Notify(new() {
                Type = ToastType.Danger,
                Message = ReceiptResult.ErrorMessage
            });
        }
    }

    public async Task CreateTemplateFromReceipt()
    {
        if (ReceiptResult?.IsSuccess != true && !string.IsNullOrEmpty(ReceiptResult?.Data.OrganizationName))
        {
            return;
        }
        
        var expenseForm = expenseDetailForm.GetExpenseForm();
        
        var template = new ExpenseTemplateFormDTO()
        {
            OrganizationName = ReceiptResult!.Data.OrganizationName!,
            CategoryId = expenseForm.CategoryId,
            TagIds = expenseForm.TagIds
        };

        CreateTemplateResult = await ExpenseTemplateService.CreateTemplate(template);

        if (CreateTemplateResult.IsSuccess)
        {
            ToastService.Notify(new()
            {
                Type = ToastType.Success,
                Message = $"Template for organization \"{ReceiptResult.Data.OrganizationName}\" successfully created."
            });
        }
        else
        {
            ToastService.Notify(new()
            {
                Type = ToastType.Danger,
                Message = CreateTemplateResult.ErrorMessage
            });
        }
    }
}
