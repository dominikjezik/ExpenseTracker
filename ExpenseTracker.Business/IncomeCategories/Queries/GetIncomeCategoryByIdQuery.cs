using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Queries;

public record GetIncomeCategoryByIdQuery(Guid CategoryId) : IRequest<IncomeCategory?>;
