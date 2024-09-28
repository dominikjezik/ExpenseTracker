using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseCategories.DTOs;

/// <summary>
/// Data Transfer Object of an ExpenseCategory entity.
/// </summary>
public class ExpenseCategoryDTO
{
    /// <summary>
    /// Id of the category.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the category.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Extension methods associated with the ExpenseCategoryDTO class.
/// </summary>
public static class ExpenseCategoryDTOExtensions
{
    public static ExpenseCategoryDTO ToDTO(this ExpenseCategory category)
    {
        return new ExpenseCategoryDTO
        {
            CategoryId = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}
