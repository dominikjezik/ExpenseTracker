namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

/// <summary>
/// Associative entity between Expense and ExpenseTag
/// </summary>
public class ExpenseExpenseTag
{
    /// <summary>
    /// Id of the Expense.
    /// </summary>
    public Guid ExpenseId { get; set; }

    /// <summary>
    /// Expense entity.
    /// </summary>
    public Expense? Expense { get; set; }

    /// <summary>
    /// Id of the ExpenseTag.
    /// </summary>
    public Guid ExpenseTagId { get; set; }

    /// <summary>
    /// ExpenseTag entity.
    /// </summary>
    public ExpenseTag? ExpenseTag { get; set; }
}
