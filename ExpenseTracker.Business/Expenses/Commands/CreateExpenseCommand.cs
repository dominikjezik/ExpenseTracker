using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Commands;

public record CreateExpenseCommand(ExpenseFormDTO ExpenseForm, Guid UserId) : IRequest<ExpenseDTO>;
