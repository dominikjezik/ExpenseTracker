using ExpenseTracker.Business.ExpenseTags.DTOs;
using MediatR;

namespace ExpenseTracker.Business.ExpenseTags.Commands;

public record CreateExpenseTagCommand(ExpenseTagFormDTO TagForm, Guid UserId) : IRequest<ExpenseTagDTO>;
