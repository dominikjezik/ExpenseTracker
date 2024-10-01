using ExpenseTracker.Business.IncomeCategories.DTOs;
using ExpenseTracker.Data.Entities.IncomesAggregate;

namespace ExpenseTracker.Business.Incomes.DTOs;

/// <summary>
/// Data Transfer Object of an Income entity.
/// </summary>
public class IncomeDTO
{
    /// <summary>
    /// Id of the income.
    /// </summary>
    public Guid IncomeId { get; set; }

    /// <summary>
    /// Date and time of creation of the income.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Readable date of creation of the income.
    /// </summary>
    public string DisplayDate => CreatedAt.ToString("dd.MM.yyyy");

    /// <summary>
    /// Amount of the income.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// Readable amount of the income.
    /// </summary>
    public string DisplayAmount => Amount.ToString("0.00");

    /// <summary>
    /// Description of the income.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Category of the income.
    /// </summary>
    public IncomeCategoryDTO? Category { get; set; } 
}

/// <summary>
/// Extension methods associated with the IncomeDTO class.
/// </summary>
public static class IncomeDTOExtensions
{
    public static IncomeDTO ToDTO(this Income income)
    {
        return new IncomeDTO
        {
            IncomeId = income.Id,
            CreatedAt = income.CreatedAt,
            Amount = income.Amount,
            Description = income.Description,
            Category = income.Category?.ToDTO(),
        };
    }
}
