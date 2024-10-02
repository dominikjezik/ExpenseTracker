using System.Net;
using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.IncomeCategories.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class IncomeCategoriesService(HttpClient httpClient) : IIncomeCategoriesService
{
    public async Task<Result<List<IncomeCategoryDTO>>> GetCategories()
    {
        var response = await httpClient.GetAsync("api/incomes/categories");
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<IncomeCategoryDTO>>.Error("Failed to get income categories.");
        }
        
        var categories = await response.Content.ReadFromJsonAsync<List<IncomeCategoryDTO>>();
        
        return Result<List<IncomeCategoryDTO>>.Success(categories!);
    }

    public async Task<Result<IncomeCategoryDTO>> CreateCategory(IncomeCategoryFormDTO categoryForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/incomes/categories", categoryForm);

        if (!response.IsSuccessStatusCode)
        {
            return Result<IncomeCategoryDTO>.Error("Failed to create category.");
        }
        
        var category = await response.Content.ReadFromJsonAsync<IncomeCategoryDTO>();
        
        return Result<IncomeCategoryDTO>.Success(category!);
    }

    public async Task<Result<object>> UpdateCategory(Guid categoryId, IncomeCategoryFormDTO categoryForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/incomes/categories/{categoryId}", categoryForm);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Income category not found.");
        }
        
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this income category.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update income category.");
        }
        
        return Result<object>.Success("Successfully updated income category.");
    }

    public async Task<Result<object>> DeleteCategory(Guid categoryId)
    {
        var response = await httpClient.DeleteAsync($"api/incomes/categories/{categoryId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Income category not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this income category.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete income category.");
        }
        
        return Result<object>.Success(new object());
    }
}
