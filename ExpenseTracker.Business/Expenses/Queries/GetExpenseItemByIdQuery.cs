using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Queries;

public record GetExpenseItemByIdQuery(Guid ExpenseId, Guid UserId) : IRequest<ExpenseDTO?>;
