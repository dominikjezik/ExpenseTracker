using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Data.Entities.IncomesAggregate;

namespace ExpenseTracker.Business.Incomes.DTOs;

/// <summary>
/// Data Transfer Object of an Income create/update form.
/// </summary>
public class IncomeFormDTO
{
    /// <summary>
    /// The date of the income.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    /// <summary>
    /// The amount of the income.
    /// </summary>
    private decimal _amount;
    
    /// <summary>
    /// The amount of the income (rounded to 2 decimal places).
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be more than 0.")]
    public decimal Amount {
        get => _amount;
        set => _amount = Math.Round(value, 2);
    }
    
    /// <summary>
    /// Description of the income.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Selected CategoryId of the income.
    /// </summary>
    public Guid? CategoryId { get; set; }
}

/// <summary>
/// Extension methods for <see cref="IncomeFormDTO"/>.
/// </summary>
public static class IncomeFormDTOExtensions
{
    public static Income ToIncome(this IncomeFormDTO form, Income? targetIncome = null)
    {
        if (targetIncome == null)
        {
            targetIncome = new Income();
        }

        targetIncome.Amount = form.Amount;
        targetIncome.CreatedAt = form.CreatedAt;
        targetIncome.Description = form.Description;
        targetIncome.CategoryId = form.CategoryId;

        return targetIncome;
    }
    
    public static IncomeFormDTO ToForm(this IncomeDTO income)
    {
        return new IncomeFormDTO
        {
            CreatedAt = income.CreatedAt,
            Amount = income.Amount,
            Description = income.Description,
            CategoryId = income.Category?.CategoryId
        };
    }
}
