using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseTags.DTOs;

/// <summary>
/// Data Transfer Object of an ExpenseTag entity.
/// </summary>
public class ExpenseTagDTO
{
    /// <summary>
    /// Id of the tag.
    /// </summary>
    public Guid TagId { get; set; }
    
    /// <summary>
    /// Name of the tag.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Id of the category the tag belongs to.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Name of the category the tag belongs to.
    /// </summary>
    public string? CategoryName { get; set; }
}

/// <summary>
/// Extension methods associated with the ExpenseTagDTO class.
/// </summary>
public static class ExpenseTagDTOExtensions
{
    public static ExpenseTagDTO ToDTO(this ExpenseTag tag)
    {
        return new ExpenseTagDTO
        {
            TagId = tag.Id,
            Name = tag.Name,
            CategoryId = tag.CategoryId,
            CategoryName = tag.Category?.Name
        };
    }
}
