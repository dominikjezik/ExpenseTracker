namespace ExpenseTracker.Business.Client.DTOs;

/// <summary>
/// Data Transfer Object for receipt item.
/// </summary>
public class ReceiptItemDTO
{
    /// <summary>
    /// Name of the item.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Price of the item.
    /// </summary>
    public decimal Price { get; set; }
}
