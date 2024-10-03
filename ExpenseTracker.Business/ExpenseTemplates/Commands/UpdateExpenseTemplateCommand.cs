using ExpenseTracker.Business.ExpenseTemplates.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTemplates.Commands;

public record UpdateExpenseTemplateCommand(ExpenseTemplateFormDTO ExpenseTemplateForm, ExpenseTemplate ExistingExpenseTemplate, Guid UserId) : IRequest;
