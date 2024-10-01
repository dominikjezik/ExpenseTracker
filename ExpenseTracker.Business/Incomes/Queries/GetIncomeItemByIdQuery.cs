using ExpenseTracker.Business.Incomes.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Queries;

public record GetIncomeItemByIdQuery(Guid IncomeId, Guid UserId) : IRequest<IncomeDTO?>;
