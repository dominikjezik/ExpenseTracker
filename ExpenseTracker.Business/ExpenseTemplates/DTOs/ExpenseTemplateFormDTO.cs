using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseTemplates.DTOs;

/// <summary>
/// Data Transfer Object of an Expense template create/update form.
/// </summary>
public class ExpenseTemplateFormDTO
{
    /// <summary>
    /// Name of the organization that the expense template is associated with.
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// Selected CategoryId of the expense.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Selected TagIds of the expense.
    /// </summary>
    public HashSet<Guid> TagIds { get; set; } = new();
}

/// <summary>
/// Extension methods for <see cref="ExpenseTemplateFormDTO"/>.
/// </summary>
public static class ExpenseTemplateFormDTOExtensions
{
    public static ExpenseTemplate ToExpenseTemplate(this ExpenseTemplateFormDTO form, ExpenseTemplate? targetExpenseTemplate = null)
    {
        if (targetExpenseTemplate == null)
        {
            targetExpenseTemplate = new ExpenseTemplate();
        }

        targetExpenseTemplate.OrganizationName = form.OrganizationName;
        targetExpenseTemplate.CategoryId = form.CategoryId;

        targetExpenseTemplate.Tags = form.TagIds.Select(tagId => new ExpenseTemplateExpenseTag { ExpenseTagId = tagId }).ToList();

        return targetExpenseTemplate;
    }
    
    public static ExpenseTemplateFormDTO ToForm(this ExpenseTemplateDTO expenseTemplate)
    {
        return new ExpenseTemplateFormDTO
        {
            OrganizationName = expenseTemplate.OrganizationName,
            CategoryId = expenseTemplate.Category?.CategoryId,
            TagIds = expenseTemplate.Tags.Select(t => t.TagId).ToHashSet()
        };
    }
}
