using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

public class ExpenseTemplate : BaseEntity<Guid>
{
    /// <summary>
    /// Name of the organization that the expense template is associated with.
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// CategoryId of the expense.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Category of the expense.
    /// </summary>
    public ExpenseCategory? Category { get; set; }

    /// <summary>
    /// Tags which are associated with the expense.
    /// </summary>
    public List<ExpenseTemplateExpenseTag> Tags { get; set; } = new();

    /// <summary>
    /// UserId which created the expense template.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User which created the expense template.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
