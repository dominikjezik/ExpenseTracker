using ExpenseTracker.Business.Incomes.DTOs;
using MediatR;

namespace ExpenseTracker.Business.Incomes.Queries;

public record GetIncomesQuery(Guid UserId, DateTime? FromDate, DateTime? ToDate) : IRequest<IEnumerable<IncomeDTO>>;
