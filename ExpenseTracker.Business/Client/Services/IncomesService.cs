using System.Net;
using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Incomes.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class IncomesService(HttpClient httpClient) : IIncomesService
{
    public async Task<Result<List<IncomeDTO>>> GetIncomes()
    {
        var response = await httpClient.GetAsync("api/incomes");
       
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<IncomeDTO>>.Error("Failed to get incomes.");
        }
        
        var incomes = await response.Content.ReadFromJsonAsync<List<IncomeDTO>>();
        
        return Result<List<IncomeDTO>>.Success(incomes!);
    }

    public async Task<Result<IncomeDTO>> GetIncomeById(Guid incomeId)
    {
        var response = await httpClient.GetAsync($"api/incomes/{incomeId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<IncomeDTO>.Error("Income not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<IncomeDTO>.Error("You are not authorized to view this income.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<IncomeDTO>.Error("Failed to get income.");
        }
        
        var income = await response.Content.ReadFromJsonAsync<IncomeDTO>();
        
        return Result<IncomeDTO>.Success(income!);
    }

    public async Task<Result<IncomeDTO>> CreateIncome(IncomeFormDTO incomeForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/incomes", incomeForm);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<IncomeDTO>.Error("Failed to create income.");
        }
        
        var income = await response.Content.ReadFromJsonAsync<IncomeDTO>();
        
        return Result<IncomeDTO>.Success(income!);
    }

    public async Task<Result<object>> UpdateIncome(Guid incomeId, IncomeFormDTO incomeForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/incomes/{incomeId}", incomeForm);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Income not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this income.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update income.");
        }
        
        return Result<object>.Success(new object());
    }

    public async Task<Result<object>> DeleteIncome(Guid incomeId)
    {
        var response = await httpClient.DeleteAsync($"api/incomes/{incomeId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Income not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this income.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete income.");
        }
        
        return Result<object>.Success(new object());
    }
}
