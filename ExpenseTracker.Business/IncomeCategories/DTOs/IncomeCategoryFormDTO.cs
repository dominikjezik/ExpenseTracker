using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Data.Entities.IncomesAggregate;

namespace ExpenseTracker.Business.IncomeCategories.DTOs;

/// <summary>
/// Data Transfer Object of an IncomeCategory create/update form.
/// </summary>
public class IncomeCategoryFormDTO
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
/// Extension methods for <see cref="IncomeCategoryFormDTO"/>.
/// </summary>
public static class IncomeCategoryFormDTOExtensions
{
    public static IncomeCategory ToIncomeCategory(this IncomeCategoryFormDTO form)
    {
        var category = new IncomeCategory
        {
            Name = form.Name,
            Description = form.Description
        };

        return category;
    }
    
    public static IncomeCategoryFormDTO ToForm(this IncomeCategoryDTO category)
    {
        return new IncomeCategoryFormDTO
        {
            Name = category.Name,
            Description = category.Description
        };
    }
}
