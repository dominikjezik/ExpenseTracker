﻿using ExpenseTracker.Data.Identity;

namespace ExpenseTracker.Data.Entities.ExpenseAggregate;

/// <summary>
/// Expense entity which represents an expense with information.
/// </summary>
public class Expense : BaseEntity<Guid>
{
    /// <summary>
    /// Date and time of creation of the expense.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Amount of the expense.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Description of the expense.
    /// </summary>
    public string? Description { get; set; }

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
    public List<ExpenseExpenseTag> Tags { get; set; } = new();

    /// <summary>
    /// UserId which created the expense.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User which created the expense.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
