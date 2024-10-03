using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Commands;

public record DeleteExpenseTemplateCommand(ExpenseTemplate ExpenseTemplate, Guid UserId) : IRequest;
