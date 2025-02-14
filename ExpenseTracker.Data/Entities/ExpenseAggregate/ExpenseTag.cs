﻿using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

/// <summary>
/// ExpenseTag entity which can be used to tag expenses.
/// </summary>
public class ExpenseTag : BaseEntity<Guid>
{
    /// <summary>
    /// Name of the tag.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// ExpenseTag may or may not belong to expense category.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Expense category to which the tag belongs.
    /// </summary>
    public ExpenseCategory? Category { get; set; }

    /// <summary>
    /// Expenses which are associated with the tag.
    /// </summary>
    public IEnumerable<ExpenseExpenseTag> ExpenseExpenseTags { get; set; } = new List<ExpenseExpenseTag>();
    
    /// <summary>
    /// ExpenseTemplates which are associated with the tag.
    /// </summary>
    public IEnumerable<ExpenseTemplateExpenseTag> ExpenseTemplateExpenseTags { get; set; } = new List<ExpenseTemplateExpenseTag>();

    /// <summary>
    /// UserId which created the tag.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User which created the tag.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
