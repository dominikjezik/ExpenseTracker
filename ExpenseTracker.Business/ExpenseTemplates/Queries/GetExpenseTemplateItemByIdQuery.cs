using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Queries;

public record GetExpenseTemplateItemByIdQuery(Guid ExpenseTemplateId, Guid UserId) : IRequest<ExpenseTemplateDTO?>;
