using ExpenseTracker.Business.IncomeCategories.DTOs;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Queries;

public record GetIncomeCategoriesQuery(Guid UserId) : IRequest<IEnumerable<IncomeCategoryDTO>>;
