using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Commands;

public record DeleteExpenseCommand(Expense Expense, Guid UserId) : IRequest;
