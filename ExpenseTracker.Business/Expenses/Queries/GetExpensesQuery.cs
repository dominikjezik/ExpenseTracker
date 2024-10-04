using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Queries;

public record GetExpensesQuery(Guid UserId, DateTime? FromDate, DateTime? ToDate) : IRequest<IEnumerable<ExpenseDTO>>;
