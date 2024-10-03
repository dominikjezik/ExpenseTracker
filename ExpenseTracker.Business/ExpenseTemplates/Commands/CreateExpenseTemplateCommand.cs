using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Commands;

public record CreateExpenseTemplateCommand(ExpenseTemplateFormDTO ExpenseTemplateForm, Guid UserId) : IRequest<ExpenseTemplateDTO>;
