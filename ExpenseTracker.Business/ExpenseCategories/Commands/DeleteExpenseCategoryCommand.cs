using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseCategories.Commands;

public record DeleteExpenseCategoryCommand(ExpenseCategory Category, Guid UserId) : IRequest;
