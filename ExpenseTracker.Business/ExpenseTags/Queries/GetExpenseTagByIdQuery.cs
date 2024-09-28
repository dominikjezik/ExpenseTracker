using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Queries;

public record GetExpenseTagByIdQuery(Guid TagId) : IRequest<ExpenseTag?>;
