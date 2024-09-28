using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Data.Entities.ExpenseAggregate;

namespace ExpenseTracker.Business.ExpenseTags.DTOs;

/// <summary>
/// Data Transfer Object of an ExpenseTag create/update form.
/// </summary>
public class ExpenseTagFormDTO
{
    /// <summary>
    /// Name of the tag.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Extension methods for <see cref="ExpenseTagFormDTO"/>.
/// </summary>
public static class ExpenseTagFormDTOExtensions
{
    public static ExpenseTag ToExpenseTag(this ExpenseTagFormDTO form)
    {
        var tag = new ExpenseTag
        {
            Name = form.Name
        };

        return tag;
    }
}
