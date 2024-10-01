using ExpenseTracker.Business.Incomes.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Queries;

public record GetIncomesQuery(Guid UserId, int Page, int Limit) : IRequest<IEnumerable<IncomeDTO>>;
