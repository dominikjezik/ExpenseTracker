using System.Net;
using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.Expenses.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class ExpensesService(HttpClient httpClient) : IExpensesService
{
    public async Task<Result<List<ExpenseDTO>>> GetExpenses()
    {
        var response = await httpClient.GetAsync("api/expenses");
       
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<ExpenseDTO>>.Error("Failed to get expenses.");
        }
        
        var expenses = await response.Content.ReadFromJsonAsync<List<ExpenseDTO>>();
        
        return Result<List<ExpenseDTO>>.Success(expenses!);
    }

    public async Task<Result<ExpenseDTO>> GetExpenseById(Guid expenseId)
    {
        var response = await httpClient.GetAsync($"api/expenses/{expenseId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<ExpenseDTO>.Error("Expense not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<ExpenseDTO>.Error("You are not authorized to view this expense.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseDTO>.Error("Failed to get expense.");
        }
        
        var expense = await response.Content.ReadFromJsonAsync<ExpenseDTO>();
        
        return Result<ExpenseDTO>.Success(expense!);
    }

    public async Task<Result<ExpenseDTO>> CreateExpense(ExpenseFormDTO expenseForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/expenses", expenseForm);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseDTO>.Error("Failed to create expense.");
        }
        
        var expense = await response.Content.ReadFromJsonAsync<ExpenseDTO>();
        
        return Result<ExpenseDTO>.Success(expense!);
    }

    public async Task<Result<object>> UpdateExpense(Guid expenseId, ExpenseFormDTO expenseForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/expenses/{expenseId}", expenseForm);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this expense.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update expense.");
        }
        
        return Result<object>.Success(new object());
    }

    public async Task<Result<object>> DeleteExpense(Guid expenseId)
    {
        var response = await httpClient.DeleteAsync($"api/expenses/{expenseId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this expense.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete expense.");
        }
        
        return Result<object>.Success(new object());
    }
}
