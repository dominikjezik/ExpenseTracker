using ExpenseTracker.Business.Incomes.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Commands;

public record CreateIncomeCommand(IncomeFormDTO IncomeForm, Guid UserId) : IRequest<IncomeDTO>;
