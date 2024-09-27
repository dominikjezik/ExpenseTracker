using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

/// <summary>
/// ExpenseCategory entity which represents a category for expenses.
/// </summary>
public class ExpenseCategory : BaseEntity<Guid>
{
    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the category.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// UserId which created the category.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User which created the category.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
