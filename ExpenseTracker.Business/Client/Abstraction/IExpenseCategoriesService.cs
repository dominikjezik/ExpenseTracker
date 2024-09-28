using ExpenseTracker.Business.Client.Helpers;
using ExpenseTracker.Business.ExpenseCategories.DTOs;

namespace ExpenseTracker.Business.Client.Abstraction;

public interface IExpenseCategoriesService
{
    Task<Result<List<ExpenseCategoryDTO>>> GetCategories();
}
