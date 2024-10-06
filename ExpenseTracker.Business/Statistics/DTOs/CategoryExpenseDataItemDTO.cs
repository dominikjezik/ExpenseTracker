namespace ExpenseTracker.Business.Statistics.DTOs;

/// <summary>
/// Represents a data item for a category expense.
/// </summary>
public class CategoryExpenseDataItemDTO
{
    public string Category { get; set; } = string.Empty;
    
    public decimal Expense { get; set; }
}
