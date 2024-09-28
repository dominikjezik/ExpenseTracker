using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Queries;

public record GetExpensesQuery(Guid UserId, int Page, int Limit) : IRequest<IEnumerable<ExpenseDTO>>;
