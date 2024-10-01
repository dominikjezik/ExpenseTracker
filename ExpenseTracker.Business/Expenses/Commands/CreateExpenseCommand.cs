using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Business.Incomes.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Commands;

public record CreateExpenseCommand(ExpenseFormDTO ExpenseForm, Guid UserId) : IRequest<ExpenseDTO>;
