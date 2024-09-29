using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseCategories.DTOs;

/// <summary>
/// Data Transfer Object of an ExpenseCategory create/update form.
/// </summary>
public class ExpenseCategoryFormDTO
{
    /// <summary>
    /// Name of the category.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the category.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Extension methods for <see cref="ExpenseCategoryFormDTO"/>.
/// </summary>
public static class ExpenseCategoryFormDTOExtensions
{
    public static ExpenseCategory ToExpenseCategory(this ExpenseCategoryFormDTO form)
    {
        var category = new ExpenseCategory
        {
            Name = form.Name,
            Description = form.Description
        };

        return category;
    }
    
    public static ExpenseCategoryFormDTO ToForm(this ExpenseCategoryDTO category)
    {
        return new ExpenseCategoryFormDTO
        {
            Name = category.Name,
            Description = category.Description
        };
    }
}
