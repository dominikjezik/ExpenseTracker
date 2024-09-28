using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Queries;

public record GetExpenseCategoriesQuery(Guid UserId) : IRequest<IEnumerable<ExpenseCategoryDTO>>;
