using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.IncomesAggregate;

/// <summary>
/// Income entity which represents an income with information.
/// </summary>
public class Income : BaseEntity<Guid>
{
    /// <summary>
    /// Date and time of creation of the income.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Amount of the income.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Description of the income.
    /// </summary>
    public string? Description { get; set; } = string.Empty;
    
    /// <summary>
    /// CategoryId of the income.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Category of the income.
    /// </summary>
    public IncomeCategory? Category { get; set; }
    
    /// <summary>
    /// UserId which created the income.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User which created the income.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
