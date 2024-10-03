using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Queries;

public record GetExpenseTemplatesQuery(Guid UserId, string? OrganizationName = null) : IRequest<IEnumerable<ExpenseTemplateDTO>>;
