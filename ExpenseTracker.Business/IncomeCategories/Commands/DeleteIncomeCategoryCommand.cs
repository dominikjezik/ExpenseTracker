using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.IncomeCategories.Commands;

public record DeleteIncomeCategoryCommand(IncomeCategory Category, Guid UserId) : IRequest;
