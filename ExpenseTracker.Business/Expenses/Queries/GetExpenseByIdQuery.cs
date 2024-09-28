using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Queries;

public record GetExpenseByIdQuery(Guid ExpenseId) : IRequest<Expense?>;
