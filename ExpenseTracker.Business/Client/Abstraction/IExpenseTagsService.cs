using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTags.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpenseTagsService
{
    Task<Result<List<ExpenseTagDTO>>> GetTags();
}
