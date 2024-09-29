using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.Expenses.DTOs;

/// <summary>
/// Data Transfer Object of an Expense create/update form.
/// </summary>
public class ExpenseFormDTO
{
    /// <summary>
    /// The date of the expense.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// The amount of the expense.
    /// </summary>
    private decimal _amount;
    
    /// <summary>
    /// The amount of the expense (rounded to 2 decimal places).
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be more than 0.")]
    public decimal Amount {
        get => _amount;
        set => _amount = Math.Round(value, 2);
    }
    
    /// <summary>
    /// Description of the expense.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Selected CategoryId of the expense.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Selected TagIds of the expense.
    /// </summary>
    public HashSet<Guid> TagIds { get; set; } = new();
}

/// <summary>
/// Extension methods for <see cref="ExpenseFormDTO"/>.
/// </summary>
public static class ExpenseFormDTOExtensions
{
    public static Expense ToExpense(this ExpenseFormDTO form, Expense? targetExpense = null)
    {
        if (targetExpense == null)
        {
            targetExpense = new Expense();
        }

        targetExpense.Amount = form.Amount;
        targetExpense.CreatedAt = form.CreatedAt;
        targetExpense.Description = form.Description;
        targetExpense.CategoryId = form.CategoryId;

        targetExpense.Tags = form.TagIds.Select(tagId => new ExpenseExpenseTag { ExpenseTagId = tagId }).ToList();

        return targetExpense;
    }
    
    public static ExpenseFormDTO ToForm(this ExpenseDTO expense)
    {
        return new ExpenseFormDTO
        {
            CreatedAt = expense.CreatedAt,
            Amount = expense.Amount,
            Description = expense.Description,
            CategoryId = expense.Category?.CategoryId,
            TagIds = expense.Tags.Select(t => t.TagId).ToHashSet()
        };
    }
}
