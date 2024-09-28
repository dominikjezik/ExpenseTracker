using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Business.Expenses.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Commands;

public record CreateExpenseCategoryCommand(ExpenseCategoryFormDTO CategoryForm, Guid UserId) : IRequest<ExpenseCategoryDTO>;
