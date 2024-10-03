using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTemplates.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpenseTemplatesService
{
    Task<Result<List<ExpenseTemplateDTO>>> GetTemplates();
    
    Task<Result<ExpenseTemplateDTO>> GetTemplateById(Guid templateId);
    
    Task<Result<List<ExpenseTemplateDTO>>> GetTemplatesByOrganizationName(string organizationName);
    
    Task<Result<ExpenseTemplateDTO>> CreateTemplate(ExpenseTemplateFormDTO templateForm);
    
    Task<Result<object>> UpdateTemplate(Guid templateId, ExpenseTemplateFormDTO templateForm);
    
    Task<Result<object>> DeleteTemplate(Guid templateId);
}
