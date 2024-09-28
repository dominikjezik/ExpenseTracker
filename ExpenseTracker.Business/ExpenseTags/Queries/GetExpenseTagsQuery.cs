using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.ExpenseTags.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Queries;

public record GetExpenseTagsQuery(Guid UserId) : IRequest<IEnumerable<ExpenseTagDTO>>;
