using ExpenseTracker.Business.ExpenseTags.DTOs;
using ExpenseTracker.Data.Entities.ExpenseAggregate;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Commands;

public record UpdateExpenseTagCommand(ExpenseTagFormDTO TagForm, ExpenseTag ExistingTag, Guid UserId) : IRequest;
