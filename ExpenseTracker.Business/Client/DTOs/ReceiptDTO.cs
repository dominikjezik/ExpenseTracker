namespace ExpenseTracker.Business.Client.DTOs;

/// <summary>
/// Data Transfer Object for Receipt loaded from API. (Contains only necessary fields)
/// </summary>
public class ReceiptDTO
{
    /// <summary>
    /// Name of the organization that is on the receipt.
    /// </summary>
    public string? OrganizationName { get; set; }
    
    /// <summary>
    /// Date and time when the receipt was created.
    /// </summary>
    public DateTime? CreateDate { get; set; }
    
    /// <summary>
    /// Total price of the receipt.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Individual items on the receipt.
    /// </summary>
    public List<ReceiptItemDTO> Items { get; set; } = new();
}
