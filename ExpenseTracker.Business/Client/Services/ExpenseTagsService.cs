using System.Net;
using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTags.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class ExpenseTagsService(HttpClient httpClient) : IExpenseTagsService
{
    public async Task<Result<List<ExpenseTagDTO>>> GetTags()
    {
        var response = await httpClient.GetAsync("api/expenses/tags");
       
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<ExpenseTagDTO>>.Error("Failed to get expense tags");
        }
        
        var tags = await response.Content.ReadFromJsonAsync<List<ExpenseTagDTO>>();
        
        return Result<List<ExpenseTagDTO>>.Success(tags!);
    }

    public async Task<Result<ExpenseTagDTO>> CreateTag(ExpenseTagFormDTO tagForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/expenses/tags", tagForm);

        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseTagDTO>.Error("Failed to create tag.");
        }
        
        var tag = await response.Content.ReadFromJsonAsync<ExpenseTagDTO>();
        
        return Result<ExpenseTagDTO>.Success(tag!);
    }

    public async Task<Result<object>> UpdateTag(Guid tagId, ExpenseTagFormDTO tagForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/expenses/tags/{tagId}", tagForm);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense tag not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this expense tag.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update expense tag.");
        }
        
        return Result<object>.Success("Successfully updated expense tag.");
    }

    public async Task<Result<object>> DeleteTag(Guid tagId)
    {
        var response = await httpClient.DeleteAsync($"api/expenses/tags/{tagId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense tag not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this expense tag.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete tag.");
        }
        
        return Result<object>.Success(new object());
    }
}
