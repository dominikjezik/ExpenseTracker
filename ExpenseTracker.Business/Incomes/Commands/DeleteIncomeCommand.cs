using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Commands;

public record DeleteIncomeCommand(Income Income, Guid UserId) : IRequest;
