using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseCategories.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class ExpenseCategoriesService(HttpClient httpClient) : IExpenseCategoriesService
{
    public async Task<Result<List<ExpenseCategoryDTO>>> GetCategories()
    {
        var response = await httpClient.GetAsync("api/expenses/categories");
       
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<ExpenseCategoryDTO>>.Error("Failed to get expense categories");
        }
        
        var categories = await response.Content.ReadFromJsonAsync<List<ExpenseCategoryDTO>>();
        
        return Result<List<ExpenseCategoryDTO>>.Success(categories!);
    }
}
