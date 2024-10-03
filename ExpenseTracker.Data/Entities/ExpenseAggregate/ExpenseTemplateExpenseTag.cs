namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

/// <summary>
/// Associative entity between ExpenseTemplate and ExpenseTag
/// </summary>
public class ExpenseTemplateExpenseTag
{
    /// <summary>
    /// Id of the ExpenseTemplate.
    /// </summary>
    public Guid ExpenseTemplateId { get; set; }

    /// <summary>
    /// ExpenseTemplate entity.
    /// </summary>
    public ExpenseTemplate? ExpenseTemplate { get; set; }

    /// <summary>
    /// Id of the ExpenseTag.
    /// </summary>
    public Guid ExpenseTagId { get; set; }

    /// <summary>
    /// ExpenseTag entity.
    /// </summary>
    public ExpenseTag? ExpenseTag { get; set; }
}
