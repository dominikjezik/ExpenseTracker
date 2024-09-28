using ExpenseTracker.Business.ExpenseCategories.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Commands;

public record UpdateExpenseCategoryCommand(ExpenseCategoryFormDTO CategoryForm, ExpenseCategory ExistingCategory, Guid UserId) : IRequest;
