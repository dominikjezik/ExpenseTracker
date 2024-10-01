using ExpenseTracker.Data.Entities.IncomesAggregate;

namespace ExpenseTracker.Business.IncomeCategories.DTOs;

/// <summary>
/// Data Transfer Object of an IncomeCategory entity.
/// </summary>
public class IncomeCategoryDTO
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
/// Extension methods associated with the IncomeCategoryDTO class.
/// </summary>
public static class IncomeCategoryDTOExtensions
{
    public static IncomeCategoryDTO ToDTO(this IncomeCategory category)
    {
        return new IncomeCategoryDTO
        {
            CategoryId = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }
}
