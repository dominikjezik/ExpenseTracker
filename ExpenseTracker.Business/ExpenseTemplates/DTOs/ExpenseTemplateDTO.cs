using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseTemplates.DTOs;

/// <summary>
/// Data Transfer Object of an Expense template.
/// </summary>
public class ExpenseTemplateDTO
{
    /// <summary>
    /// Name of the organization that the expense template is associated with.
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;
    
    /// <summary>
    /// Id of the expense template.
    /// </summary>
    public Guid ExpenseTemplateId { get; set; }

    /// <summary>
    /// Category of the expense.
    /// </summary>
    public ExpenseCategoryDTO? Category { get; set; } 
    
    /// <summary>
    /// Expense tags.
    /// </summary>
    public IEnumerable<ExpenseTagDTO> Tags { get; set; } = new List<ExpenseTagDTO>();
}

/// <summary>
/// Extension methods associated with the ExpenseDTO class.
/// </summary>
public static class ExpenseTemplateDTOExtensions
{
    public static ExpenseTemplateDTO ToDTO(this ExpenseTemplate expenseTemplate)
    {
        return new ExpenseTemplateDTO
        {
            ExpenseTemplateId = expenseTemplate.Id,
            OrganizationName = expenseTemplate.OrganizationName,
            Category = expenseTemplate.Category?.ToDTO(),
            Tags = expenseTemplate.Tags.Select(t => t.ExpenseTag?.ToDTO() ?? new ExpenseTagDTO())
        };
    }
}
