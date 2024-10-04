using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseTags.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpenseTagsService
{
    Task<Result<List<ExpenseTagDTO>>> GetTags();

    Task<Result<List<ExpenseTagDTO>>> GetTagsByCategory(Guid? categoryId, bool includeGeneral = true);
        
    Task<Result<ExpenseTagDTO>> CreateTag(ExpenseTagFormDTO tagForm);

    Task<Result<object>> UpdateTag(Guid tagId, ExpenseTagFormDTO tagForm);
    
    Task<Result<object>> DeleteTag(Guid tagId);
}
