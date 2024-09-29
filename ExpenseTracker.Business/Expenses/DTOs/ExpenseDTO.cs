using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.Expenses.DTOs;

/// <summary>
/// Data Transfer Object of an Expense entity.
/// </summary>
public class ExpenseDTO
{
    /// <summary>
    /// Id of the expense.
    /// </summary>
    public Guid ExpenseId { get; set; }

    /// <summary>
    /// Date and time of creation of the expense.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Readable date of creation of the expense.
    /// </summary>
    public string DisplayDate => CreatedAt.ToString("dd.MM.yyyy");

    /// <summary>
    /// Amount of the expense.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Readable amount of the expense.
    /// </summary>
    public string DisplayAmount => Amount.ToString("0.00");

    /// <summary>
    /// Description of the expense.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Category of the expense.
    /// </summary>
    public ExpenseCategoryDTO? Category { get; set; } 
    
    /// <summary>
    /// Expense tags.
    /// </summary>
    public IEnumerable<ExpenseTagDTO> Tags { get; set; } = new List<ExpenseTagDTO>();
}

/// <summary>
/// Extension methods associated with the ExpenseDTO class.
/// </summary>
public static class ExpenseDTOExtensions
{
    public static ExpenseDTO ToDTO(this Expense expense)
    {
        return new ExpenseDTO
        {
            ExpenseId = expense.Id,
            CreatedAt = expense.CreatedAt,
            Amount = expense.Amount,
            Description = expense.Description,
            Category = expense.Category?.ToDTO(),
            Tags = expense.Tags.Select(t => t.ExpenseTag?.ToDTO() ?? new ExpenseTagDTO())
        };
    }
}
