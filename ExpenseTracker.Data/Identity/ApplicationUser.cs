using ExpenseTracker.Data.Entities.ExpenseAggregate;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Data.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// List of user's expenses.
    /// </summary>
    public IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
}
