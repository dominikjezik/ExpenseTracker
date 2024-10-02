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
        var result = await ReceiptService.GetReceiptByCode(ReceiptCode!);
        
        if (result.IsSuccess)
        {
            LoadedExpense = new ExpenseDTO
            {
                CreatedAt = result.Data.CreateDate ?? DateTime.Now,
                Amount = result.Data.TotalPrice,
                Description = result.Data.Items.Count == 1 ? result.Data.Items[0].Name : string.Empty
            };
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
