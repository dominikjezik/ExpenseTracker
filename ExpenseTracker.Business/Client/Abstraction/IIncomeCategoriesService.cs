using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.IncomeCategories.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IIncomeCategoriesService
{
    Task<Result<List<IncomeCategoryDTO>>> GetCategories();
    
    Task<Result<IncomeCategoryDTO>> CreateCategory(IncomeCategoryFormDTO categoryForm);
    
    Task<Result<object>> UpdateCategory(Guid categoryId, IncomeCategoryFormDTO categoryForm);
    
    Task<Result<object>> DeleteCategory(Guid categoryId);
}
