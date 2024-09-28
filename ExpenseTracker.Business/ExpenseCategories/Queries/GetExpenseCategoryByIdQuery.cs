using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Queries;

public record GetExpenseCategoryByIdQuery(Guid CategoryId) : IRequest<ExpenseCategory?>;
