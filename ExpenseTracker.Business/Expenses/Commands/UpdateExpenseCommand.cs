using ExpenseTracker.Business.Expenses.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.Expenses.Commands;

public record UpdateExpenseCommand(ExpenseFormDTO ExpenseForm, Expense ExistingExpense, Guid UserId) : IRequest;
