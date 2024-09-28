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
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// The amount of the expense.
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be more than 0.")]
    public decimal Amount { get; set; }
    
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
    public IEnumerable<Guid> TagIds { get; set; } = new List<Guid>();
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
        
        var tags = form.TagIds
            .Select(tagId => new ExpenseExpenseTag { ExpenseTagId = tagId })
            .ToList();
        
        targetExpense.Amount = form.Amount;
        targetExpense.CreatedAt = form.CreatedAt;
        targetExpense.Description = form.Description;
        targetExpense.CategoryId = form.CategoryId;
        targetExpense.Tags = tags;
        
        return targetExpense;
    }
}