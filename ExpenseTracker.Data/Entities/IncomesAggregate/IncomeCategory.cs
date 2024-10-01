using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.IncomesAggregate;

/// <summary>
/// IncomeCategory entity which represents a category for incomes.
/// </summary>
public class IncomeCategory : BaseEntity<Guid>
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
