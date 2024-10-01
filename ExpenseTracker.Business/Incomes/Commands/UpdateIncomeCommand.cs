using ExpenseTracker.Business.Incomes.DTOs;
using ExpenseTracker.Data.Entities.IncomesAggregate;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Commands;

public record UpdateIncomeCommand(IncomeFormDTO IncomeForm, Income ExistingIncome, Guid UserId) : IRequest;
