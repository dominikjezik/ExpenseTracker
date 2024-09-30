namespace ExpenseTracker.Business.Statistics.DTOs;

/// <summary>
/// Represents a data item of one month (incomes and expenses for month).
/// </summary>
public class MonthDataItemDTO
{
    public int Month { get; set; }
    
    public decimal Incomes { get; set; }
    
    public decimal Expenses { get; set; }
}
