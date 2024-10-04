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
    
    /// <summary>
    /// Id of the category the tag belongs to.
    /// </summary>
    public Guid? CategoryId { get; set; }
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
            Name = form.Name,
            CategoryId = form.CategoryId
        };

        return tag;
    }
    
    public static ExpenseTagFormDTO ToForm(this ExpenseTagDTO tag)
    {
        return new ExpenseTagFormDTO
        {
            Name = tag.Name,
            CategoryId = tag.CategoryId
        };
    }
}
