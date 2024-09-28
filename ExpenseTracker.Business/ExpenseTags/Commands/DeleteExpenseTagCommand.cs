using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Commands;

public record DeleteExpenseTagCommand(ExpenseTag Tag, Guid UserId) : IRequest;
