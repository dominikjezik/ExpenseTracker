using System.Net;
using System.Net.Http.Json;
using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;

namespace ExpenseTracker.Business.Client.Services;

public class ExpenseTemplatesService(HttpClient httpClient) : IExpenseTemplatesService
{
    public async Task<Result<List<ExpenseTemplateDTO>>> GetTemplates()
    {
        var response = await httpClient.GetAsync("api/expenses/templates");
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<ExpenseTemplateDTO>>.Error("Failed to get expense templates.");
        }
        
        var templates = await response.Content.ReadFromJsonAsync<List<ExpenseTemplateDTO>>();
        
        return Result<List<ExpenseTemplateDTO>>.Success(templates!);
    }
    
    public async Task<Result<ExpenseTemplateDTO>> GetTemplateById(Guid templateId)
    {
        var response = await httpClient.GetAsync($"api/expenses/templates/{templateId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<ExpenseTemplateDTO>.Error("Expense template not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<ExpenseTemplateDTO>.Error("You are not authorized to view this expense template.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseTemplateDTO>.Error("Failed to get expense template.");
        }
        
        var template = await response.Content.ReadFromJsonAsync<ExpenseTemplateDTO>();
        
        return Result<ExpenseTemplateDTO>.Success(template!);
    }

    public async Task<Result<List<ExpenseTemplateDTO>>> GetTemplatesByOrganizationName(string organizationName)
    {
        var response = await httpClient.GetAsync($"api/expenses/templates?organizationName={WebUtility.UrlEncode(organizationName)}");
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<ExpenseTemplateDTO>>.Error("Failed to get expense template.");
        }
        
        var template = await response.Content.ReadFromJsonAsync<List<ExpenseTemplateDTO>>();
        
        return Result<List<ExpenseTemplateDTO>>.Success(template!);
    }

    public async Task<Result<ExpenseTemplateDTO>> CreateTemplate(ExpenseTemplateFormDTO templateForm)
    {
        var response = await httpClient.PostAsJsonAsync("api/expenses/templates", templateForm);
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<ExpenseTemplateDTO>.Error("Failed to create expense template.");
        }
        
        var template = await response.Content.ReadFromJsonAsync<ExpenseTemplateDTO>();
        
        return Result<ExpenseTemplateDTO>.Success(template!);
    }
    
    public async Task<Result<object>> UpdateTemplate(Guid templateId, ExpenseTemplateFormDTO templateForm)
    {
        var response = await httpClient.PutAsJsonAsync($"api/expenses/templates/{templateId}", templateForm);
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense template not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to update this expense template.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to update expense template.");
        }
        
        return Result<object>.Success(new object());
    }
    
    public async Task<Result<object>> DeleteTemplate(Guid templateId)
    {
        var response = await httpClient.DeleteAsync($"api/expenses/templates/{templateId}");
        
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Result<object>.Error("Expense template not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return Result<object>.Error("You are not authorized to delete this expense template.");
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<object>.Error("Failed to delete expense template.");
        }
        
        return Result<object>.Success(new object());
    }
}
