using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Queries;

public record GetExpenseTemplateByIdQuery(Guid ExpenseTemplateId) : IRequest<ExpenseTemplate?>;
