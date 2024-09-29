using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseCategories.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpenseCategoriesService
{
    Task<Result<List<ExpenseCategoryDTO>>> GetCategories();
    
    Task<Result<ExpenseCategoryDTO>> CreateCategory(ExpenseCategoryFormDTO categoryForm);

    Task<Result<object>> UpdateCategory(Guid categoryId, ExpenseCategoryFormDTO categoryForm);
    
    Task<Result<object>> DeleteCategory(Guid categoryId);
}
