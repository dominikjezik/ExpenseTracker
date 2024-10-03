using System.Net;
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
            return Result<List<ExpenseCategoryDTO>>.Error("Failed to get expense categories.");
        }
        
        var categories = await response.Content.ReadFromJsonAsync<List<ExpenseCategoryDTO>>();
        
        return Result<List<ExpenseCategoryDTO>>.Success(categories!);
    }

    public async Task<Result<ExpenseCategoryDTO>> CreateCategory(ExpenseCategoryFormDTO categoryForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/expenses/categories", categoryForm);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var msg = await response.Content.ReadAsStringAsync();
            return Result<ExpenseCategoryDTO>.Error(msg);
        }

        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseCategoryDTO>.Error("Failed to create category.");
        }
        
        var category = await response.Content.ReadFromJsonAsync<ExpenseCategoryDTO>();
        
        return Result<ExpenseCategoryDTO>.Success(category!);
    }

    public async Task<Result<object>> UpdateCategory(Guid categoryId, ExpenseCategoryFormDTO categoryForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/expenses/categories/{categoryId}", categoryForm);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var msg = await response.Content.ReadAsStringAsync();
            return Result<object>.Error(msg);
        }
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense category not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this expense category.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update expense category.");
        }
        
        return Result<object>.Success("Successfully updated expense category.");
    }

    public async Task<Result<object>> DeleteCategory(Guid categoryId)
    {
        var response = await httpClient.DeleteAsync($"api/expenses/categories/{categoryId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense category not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this expense category.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete expense.");
        }
        
        return Result<object>.Success(new object());
    }
}
