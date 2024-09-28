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
}
